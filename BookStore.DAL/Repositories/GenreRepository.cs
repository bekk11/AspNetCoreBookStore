using BookStore.DAL.Interfaces;
using BookStore.Domain.Entity;
using BookStore.Domain.Templates;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.DAL.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly BookStoreDbContext _dbContext;
    private readonly ILogger<GenreRepository> _logger;

    public GenreRepository(BookStoreDbContext dbContext, ILogger<GenreRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    // Gets all genres from db
    public async Task<List<Genre>> GetAll(IHttpContextAccessor accessor)
    {
        try
        {
            return _dbContext.Genres.ToList();
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

    // Creates new Genre In DB
    public async Task<Genre> Create(GenreTemplate template, IHttpContextAccessor accessor)
    {
        try
        {
            var genre = new Genre
            {
                Name = template.Name
            };

            await _dbContext.Genres.AddAsync(genre);
            await _dbContext.SaveChangesAsync();

            return genre;
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

    // Gets genre from DB by id
    public async Task<Genre?> GetById(long id, IHttpContextAccessor accessor)
    {
        try
        {
            return await _dbContext.Genres.FirstOrDefaultAsync(x => x.Id == id);
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

    // Updates genre from DB by id
    public async Task<Genre?> UpdateById(long id, GenreTemplate template, IHttpContextAccessor accessor)
    {
        try
        {
            var genre = await _dbContext.Genres.FirstOrDefaultAsync(x => x.Id == id);

            if (genre == null) return null;

            genre.Name = template.Name;

            await _dbContext.SaveChangesAsync();

            return genre;
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

    // Deletes genre from db by id
    public async Task<bool> DeleteById(long id, IHttpContextAccessor accessor)
    {
        try
        {
            var genre = await _dbContext.Genres.FirstOrDefaultAsync(x => x.Id == id);

            if (genre == null) return false;

            _dbContext.Genres.Remove(genre);
            await _dbContext.SaveChangesAsync();

            return true;
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
}