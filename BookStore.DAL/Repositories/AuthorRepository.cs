using BookStore.DAL.Interfaces;
using BookStore.Domain.Entity;
using BookStore.Domain.Templates;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;

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
            return await _dbContext.Authors.FromSql($"SELECT * FROM public.author").ToListAsync();
            // return await _dbContext.Authors.FromSql($"CALL author_list").ToListAsync();
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
    public async Task Create(AuthorTemplate template, IHttpContextAccessor accessor)
    {
        try
        {
            int result = await _dbContext.Database.ExecuteSqlAsync(
                $"INSERT INTO public.author(firstname, lastname, age) VALUES ({template.Firstname}, {template.Lastname}, {template.Age})");

            if (result > 0)
            {
                return;
            }

            throw new Exception("Something went wrong while inserting data to db");
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
            // return await _dbContext.Authors.FirstOrDefaultAsync(author => author.Id == id);
            return _dbContext.Authors.FromSql($"SELECT * FROM public.author where id = {id}").FirstOrDefault();
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
    public async Task UpdateById(long id, AuthorTemplate template, IHttpContextAccessor accessor)
    {
        try
        {
            int result = await _dbContext.Database.ExecuteSqlAsync(
                $"UPDATE public.author SET firstname = {template.Firstname}, lastname = {template.Lastname}, age = {template.Age} WHERE id ={id}");

            if (result > 0)
            {
                return;
            }

            throw new Exception("Something went wrong while updating row");
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
            int res = await _dbContext.Database.ExecuteSqlAsync($"DELETE FROM public.author WHERE id = {id}");

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