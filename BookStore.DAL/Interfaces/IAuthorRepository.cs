using BookStore.Domain.Entity;
using BookStore.Domain.Templates;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BookStore.DAL.Interfaces;

public interface IAuthorRepository
{
    Task<List<Author>> GetAll(IHttpContextAccessor accessor);

    Task<Author> Create(AuthorTemplate template, IHttpContextAccessor accessor);

    Task<Author?> GetById(long id, IHttpContextAccessor accessor);

    Task<Author?> UpdateById(long id, AuthorTemplate template, IHttpContextAccessor accessor);

    Task<bool> DeleteById(long id, IHttpContextAccessor accessor);
}