using day1.Models;
using day1.Reprository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace day1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenerateProduct repo;

        public ProductController(IGenerateProduct repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public ActionResult GetProduct()
        {
            var Product = repo.GetAll();
            return Ok(Product);
        }


        [HttpGet("{id:int}")]
        public IActionResult GetProduct(int id)
        {
            return Ok(repo.GetByID(id));
        }

        [HttpPost("{id:int}")]
        public IActionResult CreateProduct(int id,[FromBody] Product newProduct)
        {
            if (newProduct == null)
            {
                return BadRequest("Error.");
            }
            var x = repo.GetByID(id);
            repo.Insert(newProduct); 
            return Ok(newProduct);
        }
    }
}
