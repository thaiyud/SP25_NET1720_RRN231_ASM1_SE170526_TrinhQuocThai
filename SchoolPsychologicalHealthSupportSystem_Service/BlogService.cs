using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolPsychologicalHealthSupportSystem;
using SchoolPsychologicalHealthSupportSystem.Models;

namespace SchoolPsychologicalHealthSupportSystem_Service
{
    public class BlogService : IBlogService
    {
        private readonly BlogRepository _blogRepository;
        public BlogService()
        {
            _blogRepository = new BlogRepository();
        }
        public async Task<List<Blog>> GetAll()
        {
            return await _blogRepository.GetAll();
        }

        public async Task<Blog> GetById(int id)
        {
            return _blogRepository.GetById(id);
        }
        public async Task<int> Create(Blog blog)
        {
            return await _blogRepository.CreateAsync(blog);
        }
        public async Task<int> Update(Blog blog)
        {
            return await _blogRepository.UpdateAsync(blog);
        }
        public async Task<bool> Delete(int id)
        {
            var item = await _blogRepository.GetByIdAsync(id);
            if (item != null)
            {
                return await _blogRepository.RemoveAsync(item);
            }
            return false;
        }

        public async Task<List<Blog>> Search(string name, string title, string cateName)
        {
            return await _blogRepository.Search(name, title, cateName);
        }
    }
}
