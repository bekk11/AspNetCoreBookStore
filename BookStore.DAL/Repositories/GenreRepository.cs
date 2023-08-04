using BookStore.DAL.Interfaces;
using BookStore.Domain.Entity;
using BookStore.Domain.Templates;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DAL.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly BookStoreDbContext _dbContext;

    public GenreRepository(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Gets all genres from db
    public async Task<List<Genre>> GetAll()
    {
        return _dbContext.Genres.ToList();
    }

    // Creates new Genre In DB
    public async Task<Genre> Create(GenreTemplate template)
    {
        var genre = new Genre
        {
            Name = template.Name
        };

        await _dbContext.Genres.AddAsync(genre);
        await _dbContext.SaveChangesAsync();

        return genre;
    }

    // Gets genre from DB by id
    public async Task<Genre?> GetById(long id)
    {
        return await _dbContext.Genres.FirstOrDefaultAsync(x => x.Id == id);
    }

    // Updates genre from DB by id
    public async Task<Genre?> UpdateById(long id, GenreTemplate template)
    {
        var genre = await _dbContext.Genres.FirstOrDefaultAsync(x => x.Id == id);

        if (genre == null) return null;

        genre.Name = template.Name;

        await _dbContext.SaveChangesAsync();

        return genre;
    }

    // Deletes genre from db by id
    public async Task<bool> DeleteById(long id)
    {
        var genre = await _dbContext.Genres.FirstOrDefaultAsync(x => x.Id == id);

        if (genre == null) return false;

        _dbContext.Genres.Remove(genre);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}