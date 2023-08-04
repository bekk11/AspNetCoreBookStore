using BookStore.Domain.Entity;
using BookStore.Domain.Templates;
using Microsoft.AspNetCore.Http;

namespace BookStore.DAL.Interfaces;

public interface IGenreRepository
{
    Task<List<Genre>> GetAll(IHttpContextAccessor accessor);

    Task<Genre> Create(GenreTemplate template, IHttpContextAccessor accessor);

    Task<Genre?> GetById(long id, IHttpContextAccessor accessor);

    Task<Genre?> UpdateById(long id, GenreTemplate template, IHttpContextAccessor accessor);

    Task<bool> DeleteById(long id, IHttpContextAccessor accessor);
}