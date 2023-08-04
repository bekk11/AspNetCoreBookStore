using BookStore.Domain.Entity;
using BookStore.Domain.Response;
using BookStore.Domain.Templates;
using Microsoft.AspNetCore.Http;

namespace BookStore.Service.Interfaces;

public interface IBookService
{
    Task<IBaseResponse<List<Book>>> ListService(IHttpContextAccessor accessor);

    Task<IBaseResponse<Book?>> CreateService(BookTemplate template, IHttpContextAccessor accessor);

    Task<IBaseResponse<Book>> GetByIdService(long id, IHttpContextAccessor accessor);

    Task<IBaseResponse<Book>> UpdateByIdService(long id, BookTemplate template, IHttpContextAccessor accessor);

    Task<IBaseResponse<Book>> DeleteByIdService(long id, IHttpContextAccessor accessor);
}