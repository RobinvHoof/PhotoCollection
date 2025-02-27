using BlazorApp.Core.Models;

namespace BlazorApp.Core.Interfaces
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetAllAsync();
        Task<Photo> AddAsync(Photo photo);
        Task<Photo?> GetPhotoByIdAsync(int id);
        Task RemovePhotoAsync(int id);
        Task UpdatePhotoAsync(Photo photo);
    }
}
