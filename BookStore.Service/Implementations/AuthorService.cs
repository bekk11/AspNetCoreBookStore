using System.Diagnostics;
using BookStore.DAL.Interfaces;
using BookStore.DAL.Repositories;
using BookStore.Domain.Entity;
using BookStore.Domain.Response;
using BookStore.Domain.Templates;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BookStore.Service.Implementations;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;
    private readonly ILogger<AuthorService> _logger;

    public AuthorService(IAuthorRepository repository, ILogger<AuthorService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    // ------------------------------------------------------------------------------------------
    public async Task<IBaseResponse<List<Author>>> ListService(IHttpContextAccessor accessor)
    {
        try
        {
            var authors = await _repository.GetAll(accessor);

            return new BaseResponse<List<Author>>
            {
                Success = true,
                Message = "SUCCESS",
                Data = authors
            };
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - ListService(IHttpContextAccessor accessor) - ErrorMessage {Message}",
                accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            return new BaseResponse<List<Author>>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }

    // ------------------------------------------------------------------------------------------
    public async Task<IBaseResponse<Author>> CreateService(AuthorTemplate template, IHttpContextAccessor accessor)
    {
        try
        {
            var author = await _repository.Create(template, accessor);
            return new BaseResponse<Author>
            {
                Success = true,
                Message = "AUTHOR SUCCESSFULLY CREATED",
                Data = author
            };
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - CreateService(AuthorTemplate template, IHttpContextAccessor accessor) - ErrorMessage {Message}",
                accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            return new BaseResponse<Author>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }

    // ------------------------------------------------------------------------------------------
    public async Task<IBaseResponse<Author>> GetByIdService(long id, IHttpContextAccessor accessor)
    {
        try
        {
            var author = await _repository.GetById(id, accessor);

            if (author == null)
                return new BaseResponse<Author>
                {
                    Success = false,
                    Message = "AUTHOR WITH THIS ID NOT FOUND",
                    Data = null
                };

            return new BaseResponse<Author>
            {
                Success = true,
                Message = "SUCCESS",
                Data = author
            };
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - GetByIdService(long id, IHttpContextAccessor accessor) - ErrorMessage {Message}",
                accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            return new BaseResponse<Author>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }

    // ------------------------------------------------------------------------------------------
    public async Task<IBaseResponse<Author>> UpdateByIdService(long id, AuthorTemplate template, IHttpContextAccessor accessor)
    {
        try
        {
            var author = await _repository.UpdateById(id, template, accessor);

            if (author == null)
                return new BaseResponse<Author>
                {
                    Success = false,
                    Message = "AUTHOR WITH THIS ID NOT FOUND",
                    Data = null
                };

            return new BaseResponse<Author>
            {
                Success = true,
                Message = "AUTHOR UPDATED SUCCESSFULLY",
                Data = author
            };
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - UpdateByIdService(long id, AuthorTemplate template, IHttpContextAccessor accessor) - ErrorMessage {Message}",
                accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            return new BaseResponse<Author>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }

    // ------------------------------------------------------------------------------------------
    public async Task<IBaseResponse<Author>> DeleteByIdService(long id, IHttpContextAccessor accessor)
    {
        try
        {
            var isFoundAuthor = await _repository.DeleteById(id, accessor);

            if (!isFoundAuthor)
                return new BaseResponse<Author>
                {
                    Success = false,
                    Message = "AUTHOR WITH THIS ID NOT FOUND",
                    Data = null
                };

            return new BaseResponse<Author>
            {
                Success = true,
                Message = "AUTHOR DELETED SUCCESSFULLY",
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
            return new BaseResponse<Author>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }
}