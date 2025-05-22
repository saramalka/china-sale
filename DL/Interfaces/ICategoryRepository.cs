using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> Get();
        public Task<Category> Get(int id);
        public Task Add(Category category);
        public Task Update(int id, Category category);
        public Task Delete(int id);
        public Task<bool> NameExist(string name);
    }
}
