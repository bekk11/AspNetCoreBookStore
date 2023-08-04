using BookStore.DAL.Interfaces;
using BookStore.Domain.Entity;
using BookStore.Domain.Templates;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DAL.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookStoreDbContext _dbContext;

    public BookRepository(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Book>> GetAll()
    {
        return _dbContext.Books.Include(x => x.Genres).ToList();
    }

    public async Task<Book?> Create(BookTemplate template)
    {
        List<Genre?> genres = new();

        foreach (var id in template.Genres)
        {
            var genre = await _dbContext.Genres.FirstOrDefaultAsync(genre => genre.Id == id);

            if (genre == null) continue;

            genres.Add(genre);
        }

        var book = new Book
        {
            Title = template.Title,
            Description = template.Description,
            CreatedAt = DateTime.UtcNow,
            AuthorId = template.AuthorId,
            ImagePath = template.ImagePath,
            Genres = genres
        };

        await _dbContext.Books.AddAsync(book);
        await _dbContext.SaveChangesAsync();

        return book;
    }

    public async Task<Book?> GetById(long id)
    {
        return await _dbContext.Books.FirstOrDefaultAsync(book => book.Id == id);
    }

    public async Task<Book?> UpdateById(long id, BookTemplate template)
    {
        var book = await _dbContext.Books.FirstOrDefaultAsync(book => book.Id == id);

        if (book == null) return null;

        List<Genre> genres = new();

        foreach (var genreId in template.Genres)
        {
            var genre = await _dbContext.Genres.FirstOrDefaultAsync(genre => genre.Id == genreId);

            if (genre == null) continue;

            genres.Add(genre);
        }

        // delete image     

        book.Title = template.Title;
        book.Description = template.Description;
        book.CreatedAt = DateTime.UtcNow;
        book.AuthorId = template.AuthorId;
        book.ImagePath = template.ImagePath;
        book.Genres = genres;

        _dbContext.Books.Update(book);
        await _dbContext.SaveChangesAsync();

        return book;
    }

    public async Task<bool> DeleteById(long id)
    {
        var book = await _dbContext.Books.FirstOrDefaultAsync(book => book.Id == id);

        if (book == null) return false;

        _dbContext.Remove(book);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}