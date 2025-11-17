using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace KandangMobil.Helpers
{
    public class UploadHelper
    {
        private readonly IWebHostEnvironment _env;

        public UploadHelper(IWebHostEnvironment env)
        {
            _env = env;
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            char[] result = new char[length];

            for (int i = 0; i < length; i++)
                result[i] = chars[random.Next(chars.Length)];

            return new string(result);
        }

        public async Task<string?> UploadFile(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
                return null;

            string uploadPath = Path.Combine(_env.WebRootPath, folder);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            string extension = Path.GetExtension(file.FileName);
            string randomName = GenerateRandomString(10);

            string uniqueFileName = $"{DateTime.Now:yyyyMMdd}_{randomName}{extension}";
            string filePath = Path.Combine(uploadPath, uniqueFileName);

            // Simpan file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return uniqueFileName;
        }

        public void DeleteFile(string folder, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return;

            string fullPath = Path.Combine(_env.WebRootPath, folder, fileName);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
