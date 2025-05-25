using BLL.Interfaces;
using DL.Entities;
using DL.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository _categoryRepository;
        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }                    
        public async Task Add(Category category)
        {
           var duplicate=await _categoryRepository.NameExist(category.Name);
            if (duplicate)
            {
                throw new DuplicateNameException("duplecate category name");
            }
            await _categoryRepository.Add(category);
        }

        public async Task Delete(int id)
        {
            var exist=await _categoryRepository.Get(id);
            if (exist == null)
            {
                throw new KeyNotFoundException($"not founc category with id {id}");
            }
            await _categoryRepository.Delete(id);
        }

        public async Task<IEnumerable<Category>> Get()
        {
            var categories=await _categoryRepository.Get();
            if(categories == null)
            {
                throw new Exception("not found categories");

            }
            return categories;
        }

        public Task<Category> Get(int id)
        {
            var category =  _categoryRepository.Get(id);
            if (category == null)
            {
                throw new Exception($"not found category with id {id}");

            }
            return category;
        }

        public Task<bool> NameExist(string name)
        {
            return _categoryRepository.NameExist(name);
        
        }

        public async Task Update(int id, Category category)
        {
           await _categoryRepository.Update(id, category);
        }
    }
}
