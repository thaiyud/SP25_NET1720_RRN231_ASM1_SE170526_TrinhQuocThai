using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SchoolPsychologicalHealthSupportSystem.Models;

namespace SchoolPsychologicalHealthSupportSystem_WebApp.Controllers
{
    public class BlogsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string APIEndPoint = "https://localhost:6969/api/";

        public BlogsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Blogs
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(APIEndPoint + "Blog");
            if (!response.IsSuccessStatusCode)
            {
                return View(new List<Blog>());
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var blogs = JsonConvert.DeserializeObject<List<Blog>>(jsonData);
            return View(blogs);
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync(APIEndPoint + $"Blog/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var blog = JsonConvert.DeserializeObject<Blog>(jsonData);
            return View(blog);
        }

        // GET: Blogs/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Blogs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(blog);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(APIEndPoint + "Blog", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(blog);
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync(APIEndPoint + $"Blog/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var blog = JsonConvert.DeserializeObject<Blog>(jsonData);
            return View(blog);
        }

        // POST: Blogs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Blog blog)
        {
            if (id != blog.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(blog);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(APIEndPoint + $"Blog/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync(APIEndPoint + $"Blog/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var blog = JsonConvert.DeserializeObject<Blog>(jsonData);
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync(APIEndPoint + $"Blog/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
