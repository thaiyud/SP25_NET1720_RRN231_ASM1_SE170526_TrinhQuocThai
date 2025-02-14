using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SchoolPsychologicalHealthSupportSystem.DBContext;
using SchoolPsychologicalHealthSupportSystem.Models;

namespace SchoolPsychologicalHealthSupportSystem_WebApp.Controllers
{

    // chua dùng được
    public class CategoriesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string APIEndPoint = "https://localhost:6969/api/";
        public CategoriesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
      

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(APIEndPoint + "Category");
            if (!response.IsSuccessStatusCode)
            {
                return View(new List<Category>());
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<Category>>(jsonData);
            return View(list);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var response = await _httpClient.GetAsync(APIEndPoint + $"Category/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<Category>(jsonData);
            return View(item);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category item)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(APIEndPoint + "Category", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(item);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync(APIEndPoint + $"Category/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<Category>(jsonData);
            return View(item);
        }


        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(APIEndPoint + $"Category/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(item);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync(APIEndPoint + $"Category/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<Blog>(jsonData);
            return View(item);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync(APIEndPoint + $"Category/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
