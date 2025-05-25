using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> Get();
        public Task<Category> Get(int id);
        public Task Add(Category category);
        public Task Update(int id, Category category);
        public Task Delete(int id);
        public Task<bool> NameExist(string name);
    }
}
