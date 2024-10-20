using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraZoneAPI.Streaming.Storage
{
    public class StorageService
    {
        public async Task<bool> Upload(IFormFile videoFile, string fileName)
        {
            try
            {
                using (var stream = new FileStream(fileName, FileMode.Create))
                {
                    await videoFile.CopyToAsync(stream);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during file upload: {ex.Message}");
                return false;
            }
        }
    }
}
