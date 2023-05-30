﻿using VivesBlog.Ui.Mvc.Core;
using VivesBlog.Ui.Mvc.Models;

namespace VivesBlog.Ui.Mvc.Services
{
    public class PersonService
    {
        private readonly VivesBlogDbContext _dbContext;

        public PersonService(VivesBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public IList<Person> Find()
        {
            return _dbContext.People
                .OrderBy(p => p.FirstName)
                .ThenBy(p => p.LastName)
                .ToList();
        }

        //Get by id
        public Person? Get(int id)
        {
            return _dbContext.People.Find(id);
        }

        //Create
        public Person? Create(Person person)
        {
            _dbContext.Add(person);
            _dbContext.SaveChanges();

            return person;
        }

        //Update
        public Person? Update(int id, Person person)
        {
            var dbPerson = _dbContext.People.Find(id);
            if (dbPerson is null)
            {
                return null;
            }

            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Email = person.Email;

            _dbContext.SaveChanges();

            return dbPerson;
        }

        //Delete
        public void Delete(int id)
        {
            var person = new Person
            {
                Id = id,
                FirstName = string.Empty,
                LastName = string.Empty,
                Email = string.Empty
            };
            _dbContext.People.Attach(person);

            _dbContext.People.Remove(person);

            _dbContext.SaveChanges();
        }
    }
}
