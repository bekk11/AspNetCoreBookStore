using BookStore.Domain.Entity;
using BookStore.Domain.Response;
using BookStore.Domain.Templates;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

[ApiController]
[Route("author")]
public class AuthorController : Controller
{
    private readonly IAuthorService _service;
    private readonly ILogger<AuthorController> _logger;
    private readonly IHttpContextAccessor _accessor;

    public AuthorController(IAuthorService service, ILogger<AuthorController> logger, IHttpContextAccessor accessor)
    {
        _service = service;
        _logger = logger;
        _accessor = accessor;
    }

    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> GetAllAuthorsAsync()
    {
        try
        {
            // return Ok("ok");
            return Ok(await _service.ListService(_accessor));
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - GetAllAuthorsAsync() - ErrorMessage {Message}",
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
    public async Task<IActionResult> CreateAuthorAsync(AuthorTemplate template)
    {
        try
        {
            return Ok(await _service.CreateService(template, _accessor));
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - CreateAuthorAsync(AuthorTemplate template) - ErrorMessage {Message}",
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
    public async Task<IActionResult> GetAuthorById(long id)
    {
        try
        {
            return Ok(await _service.GetByIdService(id, _accessor));
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - GetAuthorById(long id) - ErrorMessage {Message}",
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
    public async Task<IActionResult> UpdateAuthorById(long id, AuthorTemplate template)
    {
        try
        {
            return Ok(await _service.UpdateByIdService(id, template, _accessor));
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - UpdateAuthorById(long id, AuthorTemplate template) - ErrorMessage {Message}",
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
    public async Task<IActionResult> DeleteAuthorById(long id)
    {
        try
        {
            return Ok(await _service.DeleteByIdService(id, _accessor));
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - DeleteAuthorById(long id) - ErrorMessage {Message}",
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