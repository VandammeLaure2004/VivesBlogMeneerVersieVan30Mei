using Microsoft.EntityFrameworkCore;
using VivesBlog.Ui.Mvc.Core;
using VivesBlog.Ui.Mvc.Models;

namespace VivesBlog.Ui.Mvc.Services
{
    public class ArticleService
    {
        private readonly VivesBlogDbContext _dbContext;

        public ArticleService(VivesBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public IList<Article> Find()
        {
            return _dbContext.Articles
                .Include(a => a.Author)
                .ToList();
        }

        //Get by id
        public Article? Get(int id)
        {
            return _dbContext.Articles
                .Include(a => a.Author)
                .FirstOrDefault(a => a.Id == id);
        }

        //Create
        public Article? Create(Article article)
        {
            article.CreatedDate = DateTime.UtcNow;

            _dbContext.Add(article);
            _dbContext.SaveChanges();

            return article;
        }

        //Update
        public Article? Update(int id, Article article)
        {
            var dbArticle = _dbContext.Articles.Find(id);
            if (dbArticle is null)
            {
                return null;
            }

            dbArticle.Title = article.Title;
            dbArticle.Description = article.Description;
            dbArticle.Content = article.Content;
            dbArticle.AuthorId = article.AuthorId;

            _dbContext.SaveChanges();

            return dbArticle;
        }

        //Delete
        public void Delete(int id)
        {
            var article = new Article
            {
                Id = id,
                Content = string.Empty,
                Description = string.Empty,
                Title = string.Empty
            };
            _dbContext.Articles.Attach(article);

            _dbContext.Articles.Remove(article);

            _dbContext.SaveChanges();
        }
    }
}
