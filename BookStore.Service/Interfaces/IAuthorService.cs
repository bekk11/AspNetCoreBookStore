using BookStore.Domain.Entity;
using BookStore.Domain.Response;
using BookStore.Domain.Templates;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BookStore.Service.Interfaces;

public interface IAuthorService
{
    Task<IBaseResponse<List<Author>>> ListService(IHttpContextAccessor accessor);

    Task<IBaseResponse<Author>> CreateService(AuthorTemplate template, IHttpContextAccessor accessor);

    Task<IBaseResponse<Author>> GetByIdService(long id, IHttpContextAccessor accessor);

    Task<IBaseResponse<Author>> UpdateByIdService(long id, AuthorTemplate template, IHttpContextAccessor accessor);

    Task<IBaseResponse<Author>> DeleteByIdService(long id, IHttpContextAccessor accessor);
}