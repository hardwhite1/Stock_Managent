
namespace MyShop.Services
{
    public class LocalFileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment environment1;
        public LocalFileUploadService(IWebHostEnvironment environment)
        {
            environment1 = environment;
        }
        public async Task<string> UploadFile(IFormFile file)
        {
            var filePath = Path.Combine(environment1.ContentRootPath, "Images", file.FileName);

            using var fileSream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(fileSream);

            return filePath;
            
        }

    }
}