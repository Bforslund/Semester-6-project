using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HotelService.Services
{
    public interface IAws3Services
    {
        void Dispose();
        Task<byte[]> DownloadFileAsync(string file);
        bool IsFileExists(string fileName);
        Task<bool> UploadFileAsync(IFormFile file);

         Task<bool> DeleteFile(string fileName);
    }
}