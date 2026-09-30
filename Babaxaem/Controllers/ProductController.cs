using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Babaxaem.Data;
using Babaxaem.Models;

namespace Babaxaem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Title == product.Title);
            if (existingProduct != null)
            {
                return BadRequest(new
                {
                    message = "Товар с таким названием уже существует"
                });
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await _context.Products.ExecuteDeleteAsync();
            
            await _context.Database.ExecuteSqlRawAsync(
                "DELETE FROM sqlite_sequence WHERE name = 'Products'"
            );

            return Ok(new
            {
                message = "Все продукты удалены"
            });
        }
    }
}