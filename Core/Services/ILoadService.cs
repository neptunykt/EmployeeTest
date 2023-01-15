using System.Threading.Tasks;
using Core.Model;
using Microsoft.AspNetCore.Http;

namespace Core.Services
{
    public interface ILoadService
    {
        Task<LoadResult> ReadAsync(IFormFile upload);
    }
}