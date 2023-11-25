using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using LandManager.Models;
using Microsoft.AspNetCore.Authorization;
using LandManager.Core;

namespace LandManager.Controllers
{
  
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var persons = string.IsNullOrEmpty(searchString)
                          ? await _personService.GetAllAsync()
                          : await _personService.SearchAsync(searchString);

            return View("~/Views/Home/PersonList.cshtml", persons);
        }

        // GET: /Person/Create
        public IActionResult Create()
        {
            return View(new Person()); // Jauns tukšs Person objekts veidlapai
        }

        // POST: /Person/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person person)
        {
            try
            {
                await _personService.AddAsync(person);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Notika kļūda saglabājot datus: " + ex.Message);
            }

            return View(person);
        }

        // GET: /Person/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _personService.GetByIdAsync(id.Value);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: /Person/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId, FirstName, LastNameOrTitle, PersonalCodeOrRegistrationNumber, Type")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            try
            {
                await _personService.UpdateAsync(person);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                if (!await PersonExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            // Ja kļūda nav notikusi, tad nevajag pārbaudīt ModelState un atgriezt View(person)
        }

        // GET: /Person/Details/1
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var summaryViewModel = await _personService.GetSummaryViewModelAsync(id.Value);
            if (summaryViewModel == null)
            {
                return NotFound();
            }

            return View(summaryViewModel);
        }

        // GET: /Person/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _personService.GetByIdAsync(id.Value);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: /Person/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _personService.GetByIdAsync(id);
            if (person != null)
            {
                await _personService.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PersonExists(int id)
        {
            var person = await _personService.GetByIdAsync(id);
            return person != null;
        }
    }
}