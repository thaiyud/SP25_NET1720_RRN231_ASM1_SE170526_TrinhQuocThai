using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SchoolPsychologicalHealthSupportSystem.Models;

namespace SchoolPsychologicalHealthSupportSystem_WebApp.Controllers
{
    public class BlogsController : Controller
    {
        //private readonly HttpClient _httpClient;
        private readonly string APIEndPoint = "https://localhost:6969/api/";



        // GET: Blogs
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion


                using (var response = await httpClient.GetAsync(APIEndPoint + "Blog"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<Blog>>(content);

                        if (result != null)
                        {
                            return View(result);
                        }
                    }
                }
            }

            return View(new List<Blog>());
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int id)
        {
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion


                using (var response = await httpClient.GetAsync(APIEndPoint + "Blog/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Blog>(content);

                        if (result != null)
                        {
                            return View(result);
                        }
                    }
                }
            }

            return View(new Blog());
        }


        public async Task<List<Category>?> GetCategory()
        {
            var topic = new List<Category>();

            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request
                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion


                using (var response = await httpClient.GetAsync(APIEndPoint + "Category/"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        topic = JsonConvert.DeserializeObject<List<Category>>(content);
                    }
                }
            }
            return topic;
        }
        // GET: Blogs/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await GetCategory(), "Id", "Name");
            return View();
        }
        // POST: Blogs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);
                        using (var response = await httpClient.PostAsJsonAsync(APIEndPoint + "Blog/", blog))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var content = await response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<int>(content);
                                if (result > 0)
                                {
                                    return RedirectToAction(nameof(Index));
                                }
                            }
                        }

                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            ViewData["TopicId"] = new SelectList(await GetCategory(), "Id", "Name", blog.CategoryId);
            return View(blog);
        }



        // GET: Blogs/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
        //get ref
        var categories = new List<Category>();

        using (var httpClient = new HttpClient())
        {
            #region Add Token to header of Request
            var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

            #endregion


            using (var response = await httpClient.GetAsync(APIEndPoint + "Category/"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                        categories = JsonConvert.DeserializeObject<List<Category>>(content);
                }
            }
        }
        //get main
        using (var httpClient = new HttpClient())
        {
            #region Add Token to header of Request


            var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

            #endregion


            using (var response = await httpClient.GetAsync(APIEndPoint + "blog/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Blog>(content);

                    if (result != null)
                    {
                        ViewData["CategoryId"] = new SelectList(categories, "Id", "Name", result.CategoryId);
                        return View(result);
                    }
                }
            }
        }
        ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "Name");
        return View(new Blog());
    }

        // POST: Blogs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Blog blog)
        {
            bool saveStatus = false;
            var topic = await this.GetCategory();
            if (ModelState.IsValid)
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        #region Add Token to header of Request

                        var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                        #endregion


                        using (var response = await httpClient.PutAsJsonAsync(APIEndPoint + "Blog/" + id, blog))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var content = await response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<int>(content);

                                if (result > 0)
                                {
                                    saveStatus = true;
                                }
                                else
                                {
                                    saveStatus = false;
                                }
                            }
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;

                }
                if (saveStatus)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["CategoryId"] = new SelectList(topic, "Id", "Name", blog.CategoryId);
                    return View(blog);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var categories = new List<Category>();

            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request
                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion


                using (var response = await httpClient.GetAsync(APIEndPoint + "Category/"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        categories = JsonConvert.DeserializeObject<List<Category>>(content);
                    }
                }
            }
            //get main
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request


                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion


                using (var response = await httpClient.GetAsync(APIEndPoint + "blog/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Blog>(content);

                        if (result != null)
                        {
                            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name", result.CategoryId);
                            return View(result);
                        }
                    }
                }
            }
            ViewData["CategoryId"] = new SelectList(categories, "TopicId", "Name");
            return View(new Blog());
        }

        // POST: Blogs/Delete/5


        public async Task<IActionResult> DeleteQuickly(string id)
        {
            bool deleteStatus = false;
            using (var httpClient = new HttpClient())
            {
                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);
                using (var response = await httpClient.DeleteAsync(APIEndPoint + "Blog/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        deleteStatus = true;
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
