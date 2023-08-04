using BookStore.DAL.Interfaces;
using BookStore.Domain.Entity;
using BookStore.Domain.Templates;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.DAL.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly BookStoreDbContext _dbContext;
    private readonly ILogger<AuthorRepository> _logger;

    public AuthorRepository(BookStoreDbContext dbContext, ILogger<AuthorRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    // Gets all Authors from DB ---------------------------------------------------------------
    public async Task<List<Author>> GetAll(IHttpContextAccessor accessor)
    {
        try
        {
            return _dbContext.Authors.ToList();
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

    // Creates new Author in DB ---------------------------------------------------------------
    public async Task<Author> Create(AuthorTemplate template, IHttpContextAccessor accessor)
    {
        try
        {
            var author = new Author
            {
                Firstname = template.Firstname,
                Lastname = template.Lastname,
                Age = template.Age
            };

            await _dbContext.Authors.AddAsync(author);
            await _dbContext.SaveChangesAsync();

            return author;
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - Create(AuthorTemplate template, IHttpContextAccessor accessor) - ErrorMessage {Message}",
                accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            throw;
        }
    }

    // Gets Author By Id ---------------------------------------------------------------
    public async Task<Author?> GetById(long id, IHttpContextAccessor accessor)
    {
        try
        {
            return await _dbContext.Authors.FirstOrDefaultAsync(author => author.Id == id);
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

    // Updates Author By Id ---------------------------------------------------------------
    public async Task<Author?> UpdateById(long id, AuthorTemplate template, IHttpContextAccessor accessor)
    {
        try
        {
            var author = await _dbContext.Authors.FirstOrDefaultAsync(author => author.Id == id);

            if (author == null) return null;

            author.Firstname = template.Firstname;
            author.Lastname = template.Lastname;
            author.Age = template.Age;

            await _dbContext.SaveChangesAsync();

            return author;
        }
        catch (Exception e)
        {
            _logger.LogError(
                "{TraceIdentifier} - UpdateById(long id, AuthorTemplate template, IHttpContextAccessor accessor) - ErrorMessage {Message}",
                accessor.HttpContext?.TraceIdentifier,
                e.Message
            );
            throw;
        }
    }

    // Deletes Author by id ---------------------------------------------------------------
    public async Task<bool> DeleteById(long id, IHttpContextAccessor accessor)
    {
        try
        {
            var author = await _dbContext.Authors.FirstOrDefaultAsync(author => author.Id == id);

            if (author == null) return false;

            _dbContext.Remove(author);
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