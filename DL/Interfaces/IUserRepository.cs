using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User> GetUserByUsername(string username);
        Task<User> AddAsync(User user);
        Task<User> GetUserFromToken();
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
    }

}
