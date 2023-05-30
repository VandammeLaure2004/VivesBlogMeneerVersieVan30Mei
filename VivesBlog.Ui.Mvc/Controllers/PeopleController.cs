﻿using Microsoft.AspNetCore.Mvc;
using VivesBlog.Ui.Mvc.Core;
using VivesBlog.Ui.Mvc.Models;
using VivesBlog.Ui.Mvc.Services;

namespace VivesBlog.Ui.Mvc.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PersonService _personService;

        public PeopleController(PersonService personService)
        {
            _personService = personService;
        }

        public IActionResult Index()
        {
            var people = _personService.Find();
            return View(people);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            _personService.Create(person);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var person = _personService.Get(id);

            if (person is null)
            {
                return RedirectToAction("Index");
            }

            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            _personService.Update(id, person);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var person = _personService.Get(id);

            if (person is null)
            {
                return RedirectToAction("Index");
            }

            return View(person);
        }

        [HttpPost("People/Delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _personService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}