using BookStore.Domain.Entity;
using BookStore.Domain.Templates;

namespace BookStore.DAL.Interfaces;

public interface IGenreRepository
{
    Task<List<Genre>> GetAll();

    Task<Genre> Create(GenreTemplate template);

    Task<Genre?> GetById(long id);

    Task<Genre?> UpdateById(long id, GenreTemplate template);

    Task<bool> DeleteById(long id);
}