using BookStore.DAL.Interfaces;
using BookStore.Domain.Entity;
using BookStore.Domain.Templates;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.DAL.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookStoreDbContext _dbContext;
    private readonly ILogger<BookRepository> _logger;

    public BookRepository(BookStoreDbContext dbContext, ILogger<BookRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    // -------------------------------------------------------------------------------
    public async Task<List<Book>> GetAll(IHttpContextAccessor accessor)
    {
        try
        {
            return _dbContext.Books.Include(x => x.Genres).ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - GetAll(IHttpContextAccessor accessor) - ErrorMessage {Message}",
                accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            throw;
        }
    }

    public async Task<Book?> Create(BookTemplate template, IHttpContextAccessor accessor)
    {
        try
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
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - Create(BookTemplate template, IHttpContextAccessor accessor) - ErrorMessage {Message}",
                accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            throw;
        }
    }

    public async Task<Book?> GetById(long id, IHttpContextAccessor accessor)
    {
        try
        {
            return await _dbContext.Books.FirstOrDefaultAsync(book => book.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - GetById(long id, IHttpContextAccessor accessor) - ErrorMessage {Message}",
                accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            throw;
        }
    }

    public async Task<Book?> UpdateById(long id, BookTemplate template, IHttpContextAccessor accessor)
    {
        try
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
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - UpdateById(long id, BookTemplate template, IHttpContextAccessor accessor) - ErrorMessage {Message}",
                accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            throw;
        }
    }

    public async Task<bool> DeleteById(long id, IHttpContextAccessor accessor)
    {
        try
        {
            var book = await _dbContext.Books.FirstOrDefaultAsync(book => book.Id == id);

            if (book == null) return false;

            _dbContext.Remove(book);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - DeleteById(long id, IHttpContextAccessor accessor) - ErrorMessage {Message}",
                accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            throw;
        }
    }
}