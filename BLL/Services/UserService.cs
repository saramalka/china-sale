using AutoMapper;
using BLL.Dto;
using DL.Entities;
using DL.Interfaces;

namespace BLL.Services
{
    public class UserService
    {

        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);

        }

        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return null;
            return _mapper.Map<UserDTO>(user);

        }

        public async Task<UserDTO> CreateAsync(UserDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            user.IsActive = true;
            var created = await _repository.AddAsync(user);
            return _mapper.Map<UserDTO>(created);

        }

        public async Task UpdateAsync(int id, UserDTO dto)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return;

            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.PasswordHash = dto.PasswordHash;
            user.Role = dto.Role;

            await _repository.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}