using common.Dto;
using BLL.Services;
using DL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase
    {
    
            private readonly GiftService _service;
        private readonly IMapper _mapper;

            public GiftController(GiftService service, IMapper mapper)
            {
                _service = service;
                _mapper = mapper;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                try
                {
                    var tasks = await _service.Get();
                    return Ok(tasks);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
            try
            {
                var gift = await _service.Get(id);
                return  Ok(gift);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
                
            }

                [HttpPost]
         
                public async Task<IActionResult> Add([FromBody] GiftDto giftDto)
                {
                    try
                    {
                        if (giftDto == null || giftDto.CategoryId == 0 || giftDto.DonorId == 0 || giftDto.GiftName == null)
                        {
                            return BadRequest("Gift data cannot be null.");
                        }
                        if (giftDto.Price < 10 || giftDto.Price > 100)
                        {
                            return BadRequest("Price must be between 10 and 100.");
                        }

                        var existingGift = await _service.TitleExists(giftDto.GiftName);
                        if (existingGift)
                        {
                            return Conflict("Gift with this name already exists.");
                        }

                        var gift = _mapper.Map<Gift>(giftDto);
                        await _service.Add(gift);
                        return CreatedAtAction(nameof(GetAll), new { id = gift.Id }, gift);
                    }
                    catch (InvalidDataException ex)
                    {
                        return BadRequest(ex.Message);
                    }
                    catch (InvalidOperationException ex)
                    {
                        return Conflict(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, "server error");
                    }
            }
        

            //[HttpPut("{id}")]
            //public async Task<IActionResult> Update(int id, [FromBody] GiftDto dto)
            //{
            //    await _service.UpdateAsync(id, dto);
            //    return NoContent();
            //}

            //[HttpDelete("{id}")]
            //public async Task<IActionResult> Delete(int id)
            //{
            //    await _service.DeleteAsync(id);
            //    return NoContent();
            //}
        }
}
