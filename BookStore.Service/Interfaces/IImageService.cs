using Microsoft.AspNetCore.Http;

namespace BookStore.Service.Interfaces;

public interface IImageService
{
    public Tuple<int, string> SaveImage(IFormFile imageFile);

    public bool DeleteImage(string imageFileName);
}