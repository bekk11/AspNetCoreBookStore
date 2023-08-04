using BookStore.DAL.Interfaces;
using BookStore.Domain.Entity;
using BookStore.Domain.Response;
using BookStore.Domain.Templates;
using BookStore.Service.Interfaces;

namespace BookStore.Service.Implementations;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IImageService _imageService;

    public BookService(IBookRepository repository, IImageService imageService)
    {
        _repository = repository;
        _imageService = imageService;
    }


    public async Task<IBaseResponse<List<Book>>> ListService()
    {
        try
        {
            var books = await _repository.GetAll();

            return new BaseResponse<List<Book>>
            {
                Success = true,
                Message = "SUCCESS",
                Data = books
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<List<Book>>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }

    public async Task<IBaseResponse<Book?>> CreateService(BookTemplate template)
    {
        try
        {
            if (template.Image != null)
            {
                Tuple<int, string> imageResult = _imageService.SaveImage(template.Image);

                if (imageResult.Item1 == 1) template.ImagePath = imageResult.Item2;
            }

            var book = await _repository.Create(template);

            return new BaseResponse<Book?>
            {
                Success = true,
                Message = "BOOK CREATED SUCCESSFULLY",
                Data = book
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<Book?>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }

    public async Task<IBaseResponse<Book>> GetByIdService(long id)
    {
        try
        {
            var book = await _repository.GetById(id);

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
            return new BaseResponse<Book>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }

    public async Task<IBaseResponse<Book>> UpdateByIdService(long id, BookTemplate template)
    {
        try
        {
            if (template.Image != null)
            {
                Tuple<int, string> imageResult = _imageService.SaveImage(template.Image);

                if (imageResult.Item1 == 1) template.ImagePath = imageResult.Item2;
            }

            var book = await _repository.UpdateById(id, template);

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
            return new BaseResponse<Book>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }

    public async Task<IBaseResponse<Book>> DeleteByIdService(long id)
    {
        try
        {
            var isFoundBook = await _repository.DeleteById(id);

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
            return new BaseResponse<Book>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }
}