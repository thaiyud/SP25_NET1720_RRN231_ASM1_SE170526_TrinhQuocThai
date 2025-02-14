using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolPsychologicalHealthSupportSystem;
using SchoolPsychologicalHealthSupportSystem.Models;
using SchoolPsychologicalHealthSupportSystem_Repo;

namespace SchoolPsychologicalHealthSupportSystem_Service
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepo _cateRepository;
        public CategoryService()
        {
            _cateRepository = new CategoryRepo();
        }
        public async Task<List<Category>> GetAll()
        {
            return await _cateRepository.GetAll();
        }

        public async Task<Category> GetById(int id)
        {
            return _cateRepository.GetById(id);
        }
        public async Task<int> Create(Category item)
        {
            return await _cateRepository.CreateAsync(item);
        }
        public async Task<int> Update(Category item)
        {
            return await _cateRepository.UpdateAsync(item);
        }
        public async Task<bool> Delete(int id)
        {
            var item = await _cateRepository.GetByIdAsync(id);
            if (item != null)
            {
                return await _cateRepository.RemoveAsync(item);
            }
            return false;
        }

        public async Task<List<Category>> Search(string name)
        {
            return await _cateRepository.Search(name);
        }
    }
}
