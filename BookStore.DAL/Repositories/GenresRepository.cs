using BookStore.DAL.Interfaces;
using BookStore.Domain.Entity;
using BookStore.Domain.Templates;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.DAL.Repositories;

public class GenresRepository : IGenreRepository
{
    private readonly BookStoreDbContext _dbContext;
    private readonly ILogger<GenresRepository> _logger;

    public GenresRepository(BookStoreDbContext dbContext, ILogger<GenresRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    // Gets all genres from db
    public async Task<List<Genre>> GetAll(IHttpContextAccessor accessor)
    {
        try
        {
            return await _dbContext.Genres.FromSqlRaw("SELECT * FROM public.genre").ToListAsync();
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
    public async Task Create(GenreTemplate template, IHttpContextAccessor accessor)
    {
        try
        {
            int res = await _dbContext.Database.ExecuteSqlAsync($"insert into public.genre(name) values({template.Name})");

            if (res > 0)
            {
                return;
            }

            throw new Exception("Something went wrong while inserting data");
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
            return await _dbContext.Genres.FromSql($"select * from public.genre where id = {id}").FirstOrDefaultAsync();
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
    public async Task UpdateById(long id, GenreTemplate template, IHttpContextAccessor accessor)
    {
        try
        {
            int res = await _dbContext.Database.ExecuteSqlAsync($"update public.genre set name = {template.Name} where id = {id}");

            if (res > 0)
            {
                return;
            }

            throw new Exception("Something went wrong while updating row");
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
            int res = await _dbContext.Database.ExecuteSqlAsync($"delete from public.genre where id = {id}");

            if (res > 0)
            {
                return true;
            }

            return false;
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