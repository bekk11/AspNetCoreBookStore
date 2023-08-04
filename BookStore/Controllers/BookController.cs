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
    private readonly IHttpContextAccessor _accessor;

    public BookController(ILogger<BookController> logger, IBookService service, IHttpContextAccessor accessor)
    {
        _logger = logger;
        _service = service;
        _accessor = accessor;
    }

    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> GetAllBooksAsync()
    {
        try
        {
            return Ok(await _service.ListService(_accessor));
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - GetAllBooksAsync() - ErrorMessage {Message}",
                _accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            return Ok(new BaseResponse<Author>
            {
                Success = false,
                Message = "Internal Server Error",
                Data = null
            });
        }
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateBookAsync([FromForm] BookTemplate template)
    {
        try
        {
            return Ok(await _service.CreateService(template, _accessor));
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - CreateBookAsync([FromForm] BookTemplate template) - ErrorMessage {Message}",
                _accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            return Ok(new BaseResponse<Author>
            {
                Success = false,
                Message = "Internal Server Error",
                Data = null
            });
        }
    }

    [HttpGet]
    [Route("get/{id}")]
    public async Task<IActionResult> GetBookById(long id)
    {
        try
        {
            return Ok(await _service.GetByIdService(id, _accessor));
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - GetBookById(long id) - ErrorMessage {Message}",
                _accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            return Ok(new BaseResponse<Author>
            {
                Success = false,
                Message = "Internal Server Error",
                Data = null
            });
        }
    }

    [HttpPut]
    [Route("update/{id}")]
    public async Task<IActionResult> UpdateBookById(long id, [FromForm] BookTemplate template)
    {
        try
        {
            return Ok(await _service.UpdateByIdService(id, template, _accessor));
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - UpdateBookById(long id, [FromForm] BookTemplate template) - ErrorMessage {Message}",
                _accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            return Ok(new BaseResponse<Author>
            {
                Success = false,
                Message = "Internal Server Error",
                Data = null
            });
        }
    }

    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<IActionResult> DeleteBookById(long id)
    {
        try
        {
            return Ok(await _service.DeleteByIdService(id, _accessor));
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - DeleteBookById(long id) - ErrorMessage {Message}",
                _accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            return Ok(new BaseResponse<Author>
            {
                Success = false,
                Message = "Internal Server Error",
                Data = null
            });
        }
    }
}