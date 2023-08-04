using BookStore.DAL;
using BookStore.DAL.Interfaces;
using BookStore.DAL.Repositories;
using BookStore.Service.Implementations;
using BookStore.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();

// Services
builder.Services.AddDbContext<BookStoreDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("PG_CONFIG"),
        b => b.MigrationsAssembly("BookStore"))
);

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middlewares
app.UseMiddleware<RequestResponseLoggingMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.Run(async context => await context.Response.WriteAsync("Hello dear readers from MiddleWare!"));

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.WebRootPath, "uploads", "images")
    ),
    RequestPath = "/resources"
});

app.UseAuthorization();

app.MapControllers();

app.Run();