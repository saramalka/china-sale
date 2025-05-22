using common.Dto;
using BLL.Interfaces;
using BLL.Services;
using DL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {

        private readonly ITicketService _service;

        public TicketController(ITicketService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tickets = await _service.Get();
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
                try
                {
                    var purchase = await _service.Get(id);
                    return purchase == null ? NotFound() : Ok(purchase);
                }
                catch (Exception ex) 
                {
                    throw ex;
                }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Ticket ticket)
        {
            try
            {
              await _service.Add(ticket);
               return Ok();
            }
            catch (Exception ex)
            {
                throw ex;   
            }
        }

      

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }

}
