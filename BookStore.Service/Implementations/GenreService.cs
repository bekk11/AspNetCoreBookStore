using BookStore.DAL.Interfaces;
using BookStore.Domain.Entity;
using BookStore.Domain.Response;
using BookStore.Domain.Templates;
using BookStore.Service.Interfaces;

namespace BookStore.Service.Implementations;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _repository;

    public GenreService(IGenreRepository repository)
    {
        _repository = repository;
    }

    public async Task<IBaseResponse<List<Genre>>> ListService()
    {
        try
        {
            var genres = await _repository.GetAll();

            return new BaseResponse<List<Genre>>
            {
                Success = true,
                Message = "SUCCESS",
                Data = genres
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<List<Genre>>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }

    public async Task<IBaseResponse<Genre>> CreateService(GenreTemplate template)
    {
        try
        {
            var genre = await _repository.Create(template);

            return new BaseResponse<Genre>
            {
                Success = true,
                Message = "GENRE CREATED SUCCESSFULLY",
                Data = genre
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<Genre>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }

    public async Task<IBaseResponse<Genre>> GetByIdService(long id)
    {
        try
        {
            var genre = await _repository.GetById(id);

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
            return new BaseResponse<Genre>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }

    public async Task<IBaseResponse<Genre>> UpdateByIdService(long id, GenreTemplate template)
    {
        try
        {
            var genre = await _repository.UpdateById(id, template);

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
                Message = "GENRE UPDATED SUCCESSFULLY",
                Data = genre
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<Genre>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }

    public async Task<IBaseResponse<Genre>> DeleteByIdService(long id)
    {
        try
        {
            var isFoundGenre = await _repository.DeleteById(id);

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
            return new BaseResponse<Genre>
            {
                Success = false,
                Message = e.Message,
                Data = null
            };
        }
    }
}