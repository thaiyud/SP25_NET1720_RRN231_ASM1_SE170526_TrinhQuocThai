using SchoolPsychologicalHealthSupportSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPsychologicalHealthSupportSystem_Service
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAll();
        Task<Category> GetById(int id);
        Task<int> Create(Category item);
        Task<int> Update(Category item);
        Task<bool> Delete(int id);
        Task<List<Category>> Search(string name);
       
    }
}
