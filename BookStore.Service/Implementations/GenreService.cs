using BookStore.DAL.Interfaces;
using BookStore.Domain.Entity;
using BookStore.Domain.Response;
using BookStore.Domain.Templates;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BookStore.Service.Implementations;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _repository;
    private readonly ILogger<GenreService> _logger;

    public GenreService(IGenreRepository repository, ILogger<GenreService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IBaseResponse<List<Genre>>> ListService(IHttpContextAccessor accessor)
    {
        try
        {
            var genres = await _repository.GetAll(accessor);

            return new BaseResponse<List<Genre>>
            {
                Success = true,
                Message = "SUCCESS",
                Data = genres
            };
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - ListService(IHttpContextAccessor accessor) - ErrorMessage {Message}",
                accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            return new BaseResponse<List<Genre>>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }

    public async Task<IBaseResponse<Genre>> CreateService(GenreTemplate template, IHttpContextAccessor accessor)
    {
        try
        {
            await _repository.Create(template, accessor);

            return new BaseResponse<Genre>
            {
                Success = true,
                Message = "GENRE CREATED SUCCESSFULLY"
            };
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - CreateService(GenreTemplate template, IHttpContextAccessor accessor) - ErrorMessage {Message}",
                accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            return new BaseResponse<Genre>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }

    public async Task<IBaseResponse<Genre>> GetByIdService(long id, IHttpContextAccessor accessor)
    {
        try
        {
            var genre = await _repository.GetById(id, accessor);

            if (genre == null)
                return new BaseResponse<Genre>
                {
                    Success = false,
                    Message = "GENRE WITH THIS ID NOT FOUND",
                    Data = null
                };

            return new BaseResponse<Genre>
            {
                Success = true,
                Message = "SUCCESS",
                Data = genre
            };
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - GetByIdService(long id, IHttpContextAccessor accessor) - ErrorMessage {Message}",
                accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            return new BaseResponse<Genre>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }

    public async Task<IBaseResponse<Genre>> UpdateByIdService(long id, GenreTemplate template, IHttpContextAccessor accessor)
    {
        try
        {
            await _repository.UpdateById(id, template, accessor);

            return new BaseResponse<Genre>
            {
                Success = true,
                Message = "GENRE UPDATED SUCCESSFULLY"
            };
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - UpdateByIdService(long id, GenreTemplate template, IHttpContextAccessor accessor) - ErrorMessage {Message}",
                accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            return new BaseResponse<Genre>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }

    public async Task<IBaseResponse<Genre>> DeleteByIdService(long id, IHttpContextAccessor accessor)
    {
        try
        {
            var isFoundGenre = await _repository.DeleteById(id, accessor);

            if (!isFoundGenre)
                return new BaseResponse<Genre>
                {
                    Success = false,
                    Message = "GENRE WITH THIS ID NOT FOUND",
                    Data = null
                };

            return new BaseResponse<Genre>
            {
                Success = true,
                Message = "GENRE DELETED SUCCESSFULLY",
                Data = null
            };
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - DeleteByIdService(long id, IHttpContextAccessor accessor) - ErrorMessage {Message}",
                accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            return new BaseResponse<Genre>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }
}