using BookStore.Domain.Entity;
using BookStore.Domain.Templates;

namespace BookStore.DAL.Interfaces;

public interface IBookRepository
{
    Task<List<Book>> GetAll();

    Task<Book?> Create(BookTemplate template);

    Task<Book?> GetById(long id);

    Task<Book?> UpdateById(long id, BookTemplate template);

    Task<bool> DeleteById(long id);
}