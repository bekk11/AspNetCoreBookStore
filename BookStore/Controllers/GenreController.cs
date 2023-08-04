using BookStore.Domain.Entity;
using BookStore.Domain.Response;
using BookStore.Domain.Templates;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

[ApiController]
[Route("genre")]
public class GenreController : Controller
{
    private readonly IGenreService _service;

    public GenreController(IGenreService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> GetAllGenresAsync()
    {
        var response = await _service.ListService();
        return Ok(response);
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateGenreAsync(GenreTemplate template)
    {
        var response = await _service.CreateService(template);
        return Ok(response);
    }

    [HttpGet]
    [Route("get/{id}")]
    public async Task<IActionResult> GetGenreById(long id)
    {
        var response = await _service.GetByIdService(id);
        return Ok(response);
    }

    [HttpPut]
    [Route("update/{id}")]
    public async Task<IActionResult> UpdateGenreById(long id, GenreTemplate template)
    {
        var response = await _service.UpdateByIdService(id, template);
        return Ok(response);
    }

    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<IActionResult> DeleteGenreById(long id)
    {
        var response = await _service.DeleteByIdService(id);
        return Ok(response);
    }
}