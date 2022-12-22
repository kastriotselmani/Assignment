
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Assignment.Services.Interfaces
{
    public interface IReadExcelFileService
    {
        Task ReadExcelAsync(IFormFile file); 
    }
}
