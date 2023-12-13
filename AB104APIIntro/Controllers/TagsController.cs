using AB104APIIntro.DAL;
using AB104APIIntro.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AB104APIIntro.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{

    private readonly AppDbContext _context;

    public TagsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var json = JsonConvert.SerializeObject(await _context.Tags.ToListAsync());

        return StatusCode(StatusCodes.Status200OK, json);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);
        if (tag is null)
            return StatusCode(StatusCodes.Status404NotFound);

        var json = JsonConvert.SerializeObject(tag);
        return StatusCode(StatusCodes.Status200OK, json);
    }
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] int id)
    {

        var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);
        if (tag is null)
            return StatusCode(StatusCodes.Status404NotFound);
        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status204NoContent);

    }
    [HttpPut]
    public async Task<IActionResult> UpdateByIdAsync([FromBody] Tag tag)
    {

        var existed = await _context.Tags.FirstOrDefaultAsync(x => x.Id == tag.Id);
        if (existed is null)
            return StatusCode(StatusCodes.Status404NotFound);
        existed.Name = tag.Name;
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status204NoContent);

    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] string tagName)
    {
        Tag tag= new()
        {
            Name = tagName
        };
        await _context.Tags.AddAsync(tag);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status204NoContent);
    }
}
