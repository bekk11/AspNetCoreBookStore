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
            return await _dbContext.Books.FromSql($"SELECT * FROM public.book").ToListAsync();
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

    public async Task Create(BookTemplate template, IHttpContextAccessor accessor)
    {
        try
        {
            var result = await _dbContext.Database.ExecuteSqlAsync(
                $"INSERT INTO public.book VALUES (default, {template.Title}, {template.Description}, {DateTime.UtcNow}, {template.ImagePath}, {template.AuthorId})");

            if (result > 0)
            {
                var book = await _dbContext.Books.FromSql($"SELECT * FROM public.book WHERE title = {template.Title}")
                    .FirstOrDefaultAsync();

                foreach (var fId in template.Genres)
                {
                    await _dbContext.Database.ExecuteSqlAsync(
                        $"INSERT INTO public.\"BookGenre\" VALUES ({book.Id}, {fId})"
                    );
                }
                return;
            }

            throw new Exception("Something went wrong while adding data");
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
            return await _dbContext.Books.FromSql($"SELECT * FROM public.book where id = {id}").FirstAsync();
            
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

    public async Task UpdateById(long id, BookTemplate template, IHttpContextAccessor accessor)
    {
        try
        {
            int result = await _dbContext.Database.ExecuteSqlAsync(
                $"UPDATE public.book SET title={template.Title}, description={template.Description}, \"imagePath\"={template.ImagePath}, \"AuthorId\"={template.AuthorId} WHERE id = {id}");

            if (result > 0)
            {
                await _dbContext.Database.ExecuteSqlAsync($"delete from public.\"BookGenre\" where \"BooksId\" = {id}");

                foreach (var gId in template.Genres)
                {
                    await _dbContext.Database.ExecuteSqlAsync(
                        $"INSERT INTO public.\"BookGenre\" VALUES ({id}, {gId})"
                    );
                }

                return;
            }


            throw new Exception("Something went wrong while updating data");
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
            int res = await _dbContext.Database.ExecuteSqlAsync($"DELETE FROM public.book WHERE id = {id}");

            if (res > 0) return true;

            return false;
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