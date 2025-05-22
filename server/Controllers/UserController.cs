using common.Dto;
using BLL.Services;
using DL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;


namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;
        private readonly IMapper _mapper;
        public UsersController(UserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var token = await _service.Login(loginDto.Username, loginDto.Password);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var user = _mapper.Map<User>(registerDto);

                await _service.Register(user);
                return Ok("User registered successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //    => Ok(await _service.Get());

        //    [HttpGet("{id}")]
        //    public async Task<IActionResult> GetById(int id)
        //    {
        //        var user = await _service.Get(id);
        //        return user == null ? NotFound() : Ok(user);
        //    }

        //    [HttpPost]
        //    public async Task<IActionResult> Create(UserDTO dto)
        //    {
        //        var created = await _service.CreateAsync(dto);
        //        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        //    }

        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> Update(int id, UserDTO dto)
        //    {
        //        await _service.UpdateAsync(id, dto);
        //        return NoContent();
        //    }

        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> Delete(int id)
        //    {
        //        await _service.DeleteAsync(id);
        //        return NoContent();
        //    }
    }

}
