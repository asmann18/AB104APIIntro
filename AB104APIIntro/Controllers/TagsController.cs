using AB104APIIntro.DAL;
using AB104APIIntro.DTOs;
using AB104APIIntro.Entities;
using AB104APIIntro.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AB104APIIntro.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{

    private readonly ITagService _service;

    public TagsController(ITagService service)
    {
        _service = service;
    }



    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {

        return StatusCode(StatusCodes.Status200OK, await _service.GetAllAsync());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        return StatusCode(StatusCodes.Status200OK, await _service.GetAsync(id));
    }
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] int id)
    {
        await _service.DeleteAsync(id);
        return StatusCode(StatusCodes.Status204NoContent);

    }
    [HttpPut]
    public async Task<IActionResult> UpdateByIdAsync([FromBody] TagPutDto tagPutDto)
    {
        await _service.UpdateAsync(tagPutDto);
        return StatusCode(StatusCodes.Status204NoContent);

    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] TagPostDto tagPostDto)
    {
        await _service.CreateAsync(tagPostDto);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}
