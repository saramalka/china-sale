using BLL.Dto;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {
     
            private readonly DonationService _service;

            public DonationController(DonationService service)
            {
                _service = service;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
                => Ok(await _service.GetAllAsync());

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var donation = await _service.GetByIDAsync(id);
                return donation == null ? NotFound() : Ok(donation);
            }

            [HttpPost]
            public async Task<IActionResult> Create(DonationDto dto)
            {
                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, DonationDto dto)
            {
                await _service.UpdateAsync(id, dto);
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
        }

    }

