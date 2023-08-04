using BookStore.Domain.Entity;
using BookStore.Domain.Response;
using BookStore.Domain.Templates;

namespace BookStore.Service.Interfaces;

public interface IGenreService
{
    Task<IBaseResponse<List<Genre>>> ListService();

    Task<IBaseResponse<Genre>> CreateService(GenreTemplate template);

    Task<IBaseResponse<Genre>> GetByIdService(long id);

    Task<IBaseResponse<Genre>> UpdateByIdService(long id, GenreTemplate template);

    Task<IBaseResponse<Genre>> DeleteByIdService(long id);
}