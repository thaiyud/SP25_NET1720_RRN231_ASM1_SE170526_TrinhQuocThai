using SchoolPsychologicalHealthSupportSystem.Models;
using SchoolPsychologicalHealthSupportSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPsychologicalHealthSupportSystem_Service
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAll();
        Task<Blog> GetById(int id);
        Task<int> Create(Blog blog);
        Task<int> Update(Blog blog);
        Task<bool> Delete(int id);
       Task<List<Blog>> Search(string name, string title, string cateName);
     
    }
}
