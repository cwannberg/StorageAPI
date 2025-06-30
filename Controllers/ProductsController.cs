using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorageAPI.Data;
using StorageAPI.DTO;
using StorageAPI.Models;

namespace StorageAPI.Controllers;

[Route("api/Products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly StorageAPIContext _context;

    public ProductsController(StorageAPIContext context)
    {
        _context = context;
    }

    // GET: api/Products
    //Hämtar alla produkter från databasen, om category är angivet i urlen hämtas bara produkterna i den kategorin
    [HttpGet] 
    public async Task<ActionResult<IEnumerable<CreateProductDto>>> GetProduct([FromQuery] string? category)
    {
        var query = _context.Product.AsQueryable();

        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(p => p.Category.ToLower() == category.ToLower());
        }

        var products = await query.ToListAsync();

        if (!products.Any())
        {
            return NotFound(); 
        }

        var productDtos = products
            .Select(p => new CreateProductDto(
                        p.Name,
                        p.Price,
                        p.Category,
                        p.Shelf,
                        p.Count,
                        p.Description
                ))
            .ToList();

        return Ok(productDtos);
    }

    // GET: api/Products/5
    [HttpGet("id/{id}")] 
    public async Task<ActionResult<Product>> GetProduct(int id) 
    {
        var product = await _context.Product.FindAsync(id); 

        if (product == null)
        {
            return NotFound(); 
        }

        return product;
    }

    //GET: api/products/stats
    [HttpGet("stats")]
    public async Task<ActionResult<ProductStatsDto>> GetProductStats()
    {
        var products =  await _context.Product.ToListAsync();

        if (!products.Any())
        {
            return Ok(new ProductStatsDto
            {
                TotalProducts = 0,
                TotalStockValue = 0,
                AveragePrice = 0
            });
        }
        var totalProducts = _context.Product.Count();
        var totalStockValue = _context.Product.Sum(p => p.Price * p.Count);
        var averagePrice = totalProducts > 0 ? _context.Product.Average(p => p.Price) : 0;

        var productStats = new ProductStatsDto
        {
            TotalProducts = totalProducts,
            TotalStockValue = totalStockValue,
            AveragePrice = averagePrice
        };

        return productStats;
    }

    // PUT: api/Products/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, Product product) 
    {
        if (id != product.Id) 
        {
            return BadRequest();
        }

        _context.Entry(product).State = EntityState.Modified; 

        try
        {
            await _context.SaveChangesAsync(); 
        }
        catch (DbUpdateConcurrencyException) 
        {
            if (!ProductExists(id))
            {
                return NotFound(); 
            }
            else
            {
                throw;
            }
        }
        return Ok(NoContent());
    }

    // POST: api/Products
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(CreateProductDto createProductDto)
    {
        var product = new Product
        {
            Name = createProductDto.Name,
            Price = createProductDto.Price,
            Category = createProductDto.Category,
            Shelf = createProductDto.Shelf,
            Count = createProductDto.Count,
            Description = createProductDto.Description
        };

        _context.Product.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProduct", new { id = product.Id }, product); 
    }

    // DELETE: api/Products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Product.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Product.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductExists(int id)
    {
        return _context.Product.Any(e => e.Id == id);
    }
}
