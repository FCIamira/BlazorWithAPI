using day1.Models;
using day1.Reprository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace day1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase 
    {
        private readonly IGenerateCategory _repository;

        public CategoryController(IGenerateCategory repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public  ActionResult  GetCategory()
        {
             var categories = _repository.GetAll();
            return Ok(categories);
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult GetCategoryById(int id)
        {
            var category = _repository.GetByID(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public IActionResult PostCategory( Category category)
        {
            if (category == null)
                return BadRequest("Invalid category data");

            _repository.Insert(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category); 
        }
        [HttpPut("{id:int}")]
        public IActionResult PutCategory(int id, [FromBody] Category updatedCategory)
        {
            if (updatedCategory == null)
                return BadRequest("Invalid request body!");

            var existingCategory = _repository.GetByID(id);
            if (existingCategory == null)
                return NotFound();

            existingCategory.Name = updatedCategory.Name;

            if (!string.IsNullOrEmpty(updatedCategory.Description))
                existingCategory.Description = updatedCategory.Description;

            _repository.Save();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            
            _repository.Delete(id);
            _repository.Save();
            return NoContent();
        }


    }
}
