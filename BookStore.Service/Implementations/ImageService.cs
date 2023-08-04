using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BookStore.Service.Implementations;

public class ImageService : IImageService
{
    private IWebHostEnvironment _environment;

    public ImageService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public Tuple<int, string> SaveImage(IFormFile imageFile)
    {
        try
        {
            // Getting Path to directory for static files
            var webrootPath = _environment.WebRootPath;
            var path = Path.Combine(webrootPath, "uploads", "images");


            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            // Getting file's extension and checking it
            var ext = Path.GetExtension(imageFile.FileName);
            string[] allowedExtensions = { ".jpg", ".png", ".jpeg" };

            if (!allowedExtensions.Contains(ext))
            {
                var msg = $"Only {string.Join(",", allowedExtensions)} extensions are allowed";
                return new Tuple<int, string>(0, msg);
            }

            // Generating unique name for the file
            var uniqueString = Guid.NewGuid().ToString();
            var newFileName = uniqueString + ext;
            var fileWithPath = Path.Combine(path, newFileName);

            // Creating File Process
            var stream = new FileStream(fileWithPath, FileMode.Create);
            imageFile.CopyTo(stream);
            stream.Close();

            return new Tuple<int, string>(1, newFileName);
        }
        catch (Exception e)
        {
            return new Tuple<int, string>(0, "Error has occured while saving file!");
        }
    }

    public bool DeleteImage(string imageFileName)
    {
        try
        {
            var wwwPath = _environment.WebRootPath;
            var path = Path.Combine(wwwPath, "uploads\\", "images\\", imageFileName);

            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }

            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}