using Microsoft.EntityFrameworkCore;
using SchoolPsychologicalHealthSupportSystem.GenericRepository;
using SchoolPsychologicalHealthSupportSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPsychologicalHealthSupportSystem_Repo
{
    public class CategoryRepo : GenericRepository<Category>
    {
        public CategoryRepo() { }

        public async Task<List<Category>> GetAll()
        {
            var list = await _context.Categories.ToListAsync();
            return list;
        }
        public async Task<List<Category>> Search(string name)
        {
            var list = await _context.Categories
                   .Where(bl =>
                   (bl.Name.Contains(name) || string.IsNullOrEmpty(name))).ToListAsync();

            return list;
        }
    }
}
