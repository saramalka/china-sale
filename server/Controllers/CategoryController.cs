using BLL.Services;
using DL;
using DL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService= categoryService;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories =await _categoryService.Get();
          if (categories == null)
          {
              return NotFound();
          }
            return Ok(categories);
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoryService.Get(id);
          
            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            try
            {
                await _categoryService.Add(category);
            }
            catch (DuplicateNameException)
            {
                return BadRequest("DuplicateNameException");
            }
            catch 
            {
                throw;
            }
           

            return NoContent();
        }

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(int id,Category category)
        {
            try { 

              await _categoryService.Update(id,category);
              return CreatedAtAction("GetCategory", new { id = category.Id }, category);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.Delete(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return BadRequest($"KeyNotFound {id}");
            }
            catch
            {
                throw;
            }
        }

        private async Task<bool> CategoryExists(string name)
        {
            try
            {
                await _categoryService.NameExist(name);
                return Ok();
            }
            catch
            {
                throw;
            }
        }
    }
}
