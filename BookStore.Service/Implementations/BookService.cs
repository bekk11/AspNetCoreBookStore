using BookStore.DAL.Interfaces;
using BookStore.Domain.Entity;
using BookStore.Domain.Response;
using BookStore.Domain.Templates;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BookStore.Service.Implementations;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IImageService _imageService;
    private readonly ILogger<BookService> _logger;

    public BookService(IBookRepository repository, IImageService imageService, ILogger<BookService> logger)
    {
        _repository = repository;
        _imageService = imageService;
        _logger = logger;
    }


    public async Task<IBaseResponse<List<Book>>> ListService(IHttpContextAccessor accessor)
    {
        try
        {
            var books = await _repository.GetAll(accessor);

            return new BaseResponse<List<Book>>
            {
                Success = true,
                Message = "SUCCESS",
                Data = books
            };
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - ListService(IHttpContextAccessor accessor) - ErrorMessage {Message}",
                accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            return new BaseResponse<List<Book>>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }

    public async Task<IBaseResponse<Book?>> CreateService(BookTemplate template, IHttpContextAccessor accessor)
    {
        try
        {
            if (template.Image != null)
            {
                Tuple<int, string> imageResult = _imageService.SaveImage(template.Image);

                if (imageResult.Item1 == 1) template.ImagePath = imageResult.Item2;
            }

            var book = await _repository.Create(template, accessor);

            return new BaseResponse<Book?>
            {
                Success = true,
                Message = "BOOK CREATED SUCCESSFULLY",
                Data = book
            };
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - CreateService(BookTemplate template, IHttpContextAccessor accessor) - ErrorMessage {Message}",
                accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            return new BaseResponse<Book?>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }

    public async Task<IBaseResponse<Book>> GetByIdService(long id, IHttpContextAccessor accessor)
    {
        try
        {
            var book = await _repository.GetById(id, accessor);

            if (book == null)
                return new BaseResponse<Book>
                {
                    Success = false,
                    Message = "BOOK WITH THIS ID NOT FOUND",
                    Data = null
                };

            return new BaseResponse<Book>
            {
                Success = true,
                Message = "SUCCESS",
                Data = book
            };
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - GetByIdService(long id, IHttpContextAccessor accessor) - ErrorMessage {Message}",
                accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            return new BaseResponse<Book>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }

    public async Task<IBaseResponse<Book>> UpdateByIdService(long id, BookTemplate template,
        IHttpContextAccessor accessor)
    {
        try
        {
            if (template.Image != null)
            {
                Tuple<int, string> imageResult = _imageService.SaveImage(template.Image);

                if (imageResult.Item1 == 1) template.ImagePath = imageResult.Item2;
            }

            var book = await _repository.UpdateById(id, template, accessor);

            if (book == null)
                return new BaseResponse<Book>
                {
                    Success = false,
                    Message = "BOOK WITH THIS ID NOT FOUND",
                    Data = null
                };

            return new BaseResponse<Book>
            {
                Success = true,
                Message = "SUCCESS",
                Data = book
            };
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - UpdateByIdService(long id, BookTemplate template, IHttpContextAccessor accessor) - ErrorMessage {Message}",
                accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            return new BaseResponse<Book>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }

    public async Task<IBaseResponse<Book>> DeleteByIdService(long id, IHttpContextAccessor accessor)
    {
        try
        {
            var isFoundBook = await _repository.DeleteById(id, accessor);

            if (!isFoundBook)
                return new BaseResponse<Book>
                {
                    Success = false,
                    Message = "BOOK WITH THIS ID NOT FOUND",
                    Data = null
                };

            return new BaseResponse<Book>
            {
                Success = true,
                Message = "BOOK DELETED SUCCESSFULLY",
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
            return new BaseResponse<Book>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }
}