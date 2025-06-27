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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
    {
        return await _context.Product.ToListAsync();
    }

    // GET: api/Products/5
    [HttpGet("{id}")] //HttpGet är ett attribut som säger att när det körs ett HTTP GET-anrop där id:et är specificerat är det denna metod som körs.
    public async Task<ActionResult<Product>> GetProduct(int id) //async betyder att körs utan att blockera maintråden, Task<T> hör ihop med att den är asynkron.
                                                                // ActionResult<Product> returnerar antingen en lista med produkter eller ett HTTP-resultat (NotFound(), BadRequest() etc.
    {
        var product = await _context.Product.FindAsync(id); //await hör ihop med att metoden är asynkron. _context är kontakten med databasen. FindAsync letar efter det specifika id:t.

        if (product == null)
        {
            return NotFound(); //Retunerar HTTP-resultatet 404 Not Found.
        }

        return product;
    }

    // PUT: api/Products/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, Product product) //Uppdaterar en befintlig produkt som har ett specifikt id i databasen.
    {
        if (id != product.Id) //Kontrollerar att idt i URL:en matchar produktens Id. T.ex. PUT /api/products/3
        {
            return BadRequest();
        }

        _context.Entry(product).State = EntityState.Modified; // Berättar att den här entiteten (produkten) har blvit uppdaterad.

        try
        {
            await _context.SaveChangesAsync(); //SaveChangesAsync förstår att en UPDATE-SQL fråga ska göras eftersom produkten är ändrad (EntityState.Modified). Försöker spara ändringarna.
        }
        catch (DbUpdateConcurrencyException) //Kan uppstå om någon annan har ändrat eller tagit bort produkten sen den laddades ner från databasen.
        {
            if (!ProductExists(id))
            {
                return NotFound(); //Finns inte produkten returneras 404 Not found.
            }
            else
            {
                throw;
            }
        }

        return NoContent(); //Allt gick bra men inget returneras. Standard vid lyckad PUT eftersom inget innehåll skickas tillbaka.
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
