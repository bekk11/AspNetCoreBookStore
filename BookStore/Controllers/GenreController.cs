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
    private readonly IHttpContextAccessor _accessor;
    private readonly ILogger<GenreController> _logger;

    public GenreController(IGenreService service, IHttpContextAccessor accessor, ILogger<GenreController> logger)
    {
        _service = service;
        _accessor = accessor;
        _logger = logger;
    }

    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> GetAllGenresAsync()
    {
        
        try
        {
            return Ok(await _service.ListService(_accessor));
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - GetAllGenresAsync() - ErrorMessage {Message}",
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
    public async Task<IActionResult> CreateGenreAsync(GenreTemplate template)
    {
        try
        {
            return Ok(await _service.CreateService(template, _accessor));
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - CreateGenreAsync(GenreTemplate template) - ErrorMessage {Message}",
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
    public async Task<IActionResult> GetGenreById(long id)
    {
        try
        {
            return Ok(await _service.GetByIdService(id, _accessor));
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - GetGenreById(long id) - ErrorMessage {Message}",
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
    public async Task<IActionResult> UpdateGenreById(long id, GenreTemplate template)
    {
        try
        {
            return Ok(await _service.UpdateByIdService(id, template, _accessor));
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - UpdateGenreById(long id, GenreTemplate template) - ErrorMessage {Message}",
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
    public async Task<IActionResult> DeleteGenreById(long id)
    {
        try
        {
            return Ok(await _service.DeleteByIdService(id, _accessor));
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - DeleteGenreById(long id) - ErrorMessage {Message}",
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