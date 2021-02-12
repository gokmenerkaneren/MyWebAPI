using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEntities;
using RestSharp;

namespace MyWebApp.Controllers
{
    public class CoursesController : Controller
    {
        private readonly RestClient _client;

        public CoursesController()
        {
            _client = new RestClient(Constants.BaseUrl);
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var request = new RestRequest("Courses", DataFormat.Json);
            var list = await _client.GetAsync<List<Course>>(request);

            return View(list);

        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = new RestRequest($"Courses/{id}", DataFormat.Json);
            var course = await _client.GetAsync<Course>(request);

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Credits")] Course course)
        {
            if (ModelState.IsValid)
            {

                var request = new RestRequest($"Courses", DataFormat.Json).AddJsonBody(course);
                await _client.PostAsync<Course>(request);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = new RestRequest($"Courses/{id}", DataFormat.Json);
            var course = await _client.GetAsync<Course>(request);

            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Credits")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var request = new RestRequest($"Courses/{id}", DataFormat.Json).AddJsonBody(course);
                    await _client.PutAsync<Course>(request);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw new DbUpdateConcurrencyException();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = new RestRequest($"Courses/{id}", DataFormat.Json);
            var course = await _client.GetAsync<Course>(request);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleteRequest = new RestRequest($"Courses/{id}", DataFormat.Json);
            await _client.DeleteAsync<Course>(deleteRequest);

            return RedirectToAction(nameof(Index));
        }

    }
}
