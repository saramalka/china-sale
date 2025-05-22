using AutoMapper;
using common.Dto;
using DL.Entities;
using DL.Interfaces;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace BLL.Services
{
    public class UserService
    {

        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly string _jwtSecret = "1858e87833b36da4ea93df26fb950af27b7a2d1cdddda825eeb443ceeae1fde11cad965006c3c7ef3e927c611e4686981ef08be19a5d38d63b8985542e8893b6";

        private readonly PasswordHasher<User> _hasher = new();      
        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
         
        }

        public async Task<string> Login(string username, string password)
        {
            var user = await _repository.GetUserByUsername(username);
            if (user == null || !VerifyPassword(user.Password, password))
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            return GenerateJwtToken(user);
        }

        public async Task Register(User user)
        {
            var existingUser = await _repository.GetUserByUsername(user.Username);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Username already exists.");
            }

            user.Password = HashPassword(user.Password);
            await _repository.AddAsync(user);
        }

        public string HashPassword(string plainPassword)
        {
            return _hasher.HashPassword(null, plainPassword);
        }

        public bool VerifyPassword(string hashedPassword, string inputPassword)
        {
            var result = _hasher.VerifyHashedPassword(null, hashedPassword, inputPassword);
            return result == PasswordVerificationResult.Success;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        //public async Task<IEnumerable<UserDto>> GetAllAsync()
        //{
        //    var users = await _repository.GetAllAsync();
        //    return _mapper.Map<IEnumerable<UserDTO>>(users);

        //}

        //public async Task<UserDTO?> GetByIdAsync(int id)
        //{
        //    var user = await _repository.GetByIdAsync(id);
        //    if (user == null) return null;
        //    return _mapper.Map<UserDTO>(user);

        //}

        //public async Task<UserDTO> CreateAsync(UserDTO dto)
        //{
        //    var user = _mapper.Map<User>(dto);
        //    user.IsActive = true;
        //    var created = await _repository.AddAsync(user);
        //    return _mapper.Map<UserDTO>(created);

        //}

        //public async Task UpdateAsync(int id, UserDTO dto)
        //{
        //    var user = await _repository.GetByIdAsync(id);
        //    if (user == null) return;

        //    user.FullName = dto.FullName;
        //    user.Email = dto.Email;
        //    user.PhoneNumber = dto.PhoneNumber;
        //    user.PasswordHash = dto.PasswordHash;
        //    user.Role = dto.Role;

        //    await _repository.UpdateAsync(user);
        //}

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}