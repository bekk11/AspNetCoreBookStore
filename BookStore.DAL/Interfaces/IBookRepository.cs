using BookStore.Domain.Entity;
using BookStore.Domain.Templates;
using Microsoft.AspNetCore.Http;

namespace BookStore.DAL.Interfaces;

public interface IBookRepository
{
    Task<List<Book>> GetAll(IHttpContextAccessor accessor);

    Task<Book?> Create(BookTemplate template, IHttpContextAccessor accessor);

    Task<Book?> GetById(long id, IHttpContextAccessor accessor);

    Task<Book?> UpdateById(long id, BookTemplate template, IHttpContextAccessor accessor);

    Task<bool> DeleteById(long id, IHttpContextAccessor accessor);
}