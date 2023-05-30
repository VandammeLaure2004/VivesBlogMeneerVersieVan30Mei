using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using VivesBlog.Ui.Mvc.Models;
using VivesBlog.Ui.Mvc.Services;

namespace VivesBlog.Ui.Mvc.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ArticleService _articleService;
        private readonly PersonService _personService;

        public ArticlesController(
            ArticleService articleService, 
            PersonService personService)
        {
            _articleService = articleService;
            _personService = personService;
        }

        public IActionResult Index()
        {
            var articles = _articleService.Find();
            return View(articles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return CreateEditView("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Article article)
        {
            if (!ModelState.IsValid)
            {
                return CreateEditView("Create", article);
            }

            _articleService.Create(article);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var article = _articleService.Get(id);

            if (article is null)
            {
                return RedirectToAction("Index");
            }

            return CreateEditView("Edit", article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, Article article)
        {
            if (!ModelState.IsValid)
            {
                return CreateEditView("Edit", article);
            }

            _articleService.Update(id, article);

            return RedirectToAction("Index");
        }

        private IActionResult CreateEditView([AspMvcView] string viewName, Article? article = null)
        {
            var people = _personService.Find();

            ViewBag.People = people;

            return View(viewName, article);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var article = _articleService.Get(id);

            if (article is null)
            {
                return RedirectToAction("Index");
            }

            return View(article);
        }

        [HttpPost("Articles/Delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _articleService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}