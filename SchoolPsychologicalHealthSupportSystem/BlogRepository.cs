using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolPsychologicalHealthSupportSystem.GenericRepository;
using SchoolPsychologicalHealthSupportSystem.Models;

namespace SchoolPsychologicalHealthSupportSystem
{
    public class BlogRepository : GenericRepository<Blog>
    {
        public BlogRepository() { }

        public async Task<List<Blog>> GetAll() 
        { 
            var blogList = await _context.Blogs.Include(b => b.Category).ToListAsync();
            return blogList;
        }
        public async Task<List<Blog>> Search(string name, string title, string cateName)
        {
            var blog = await _context.Blogs
                   .Include(b => b.Category)
                   .Where(bl =>
                   (bl.Name.Contains(name) || string.IsNullOrEmpty(name))&&
                   (bl.Title.Contains(title) || string.IsNullOrEmpty(title))&&
                   (bl.Category.Name.Contains(cateName) || string.IsNullOrEmpty(cateName))).ToListAsync();

            return blog;
        }

    }
}
