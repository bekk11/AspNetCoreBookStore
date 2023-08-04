using BookStore.Domain.Entity;
using BookStore.Domain.Response;
using BookStore.Domain.Templates;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

[ApiController]
[Route("book")]
public class BookController : Controller
{
    private readonly ILogger<BookController> _logger;
    private readonly IBookService _service;

    public BookController(ILogger<BookController> logger, IBookService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> GetAllBooksAsync()
    {
        _logger.LogInformation("List Books");
        var response = await _service.ListService();
        return Ok(response);
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateBookAsync([FromForm] BookTemplate template)
    {
        _logger.LogInformation("Create Books");
        var response = await _service.CreateService(template);
        return Ok(response);
    }

    [HttpGet]
    [Route("get/{id}")]
    public async Task<IActionResult> GetBookById(long id)
    {
        _logger.LogInformation("Get Books");
        var response = await _service.GetByIdService(id);
        return Ok(response);
    }

    [HttpPut]
    [Route("update/{id}")]
    public async Task<IActionResult> UpdateBookById(long id, [FromForm] BookTemplate template)
    {
        var response = await _service.UpdateByIdService(id, template);
        return Ok(response);
    }

    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<IActionResult> DeleteBookById(long id)
    {
        var response = await _service.DeleteByIdService(id);
        return Ok(response);
    }
}