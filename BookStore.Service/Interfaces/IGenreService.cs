using BookStore.Domain.Entity;
using BookStore.Domain.Response;
using BookStore.Domain.Templates;
using Microsoft.AspNetCore.Http;

namespace BookStore.Service.Interfaces;

public interface IGenreService
{
    Task<IBaseResponse<List<Genre>>> ListService(IHttpContextAccessor accessor);

    Task<IBaseResponse<Genre>> CreateService(GenreTemplate template, IHttpContextAccessor accessor);

    Task<IBaseResponse<Genre>> GetByIdService(long id, IHttpContextAccessor accessor);

    Task<IBaseResponse<Genre>> UpdateByIdService(long id, GenreTemplate template, IHttpContextAccessor accessor);

    Task<IBaseResponse<Genre>> DeleteByIdService(long id, IHttpContextAccessor accessor);
}