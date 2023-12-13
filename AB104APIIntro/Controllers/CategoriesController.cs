using AB104APIIntro.DAL;
using AB104APIIntro.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AB104APIIntro.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var json = JsonConvert.SerializeObject(await _context.Categories.ToListAsync());

        return StatusCode(StatusCodes.Status200OK, json);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category is null)
            return StatusCode(StatusCodes.Status404NotFound);

        var json = JsonConvert.SerializeObject(category);
        return StatusCode(StatusCodes.Status200OK, json);
    }
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] int id)
    {

        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category is null)
            return StatusCode(StatusCodes.Status404NotFound);
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status204NoContent);

    }
    [HttpPut]
    public async Task<IActionResult> UpdateByIdAsync([FromBody] Category category)
    {

        var existed = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
        if (existed is null)
            return StatusCode(StatusCodes.Status404NotFound);
        existed.Name = category.Name;
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status204NoContent);

    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] string categoryName)
    {
        Category category = new()
        {
            Name = categoryName
        };
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status204NoContent);
    }
}
