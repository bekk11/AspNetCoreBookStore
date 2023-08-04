using BookStore.Domain.Entity;
using BookStore.Domain.Response;
using BookStore.Domain.Templates;

namespace BookStore.Service.Interfaces;

public interface IBookService
{
    Task<IBaseResponse<List<Book>>> ListService();

    Task<IBaseResponse<Book?>> CreateService(BookTemplate template);

    Task<IBaseResponse<Book>> GetByIdService(long id);

    Task<IBaseResponse<Book>> UpdateByIdService(long id, BookTemplate template);

    Task<IBaseResponse<Book>> DeleteByIdService(long id);
}