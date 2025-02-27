using BlazorApp.Core.Interfaces;
using BlazorApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Infrastructure.Repositories
{
    public class PhotoDbRepository : IPhotoRepository
    {
        private readonly PhotoContext _photoContext;
        public PhotoDbRepository(PhotoContext context) 
        { 
            _photoContext = context;
        }

        public Task<IEnumerable<Photo>> GetAllAsync()
        {
            return Task.FromResult(_photoContext.Photos.AsEnumerable());
        }

        public async Task<Photo> AddAsync(Photo photo)
        {
            _photoContext.Photos.Add(photo);

            await _photoContext.SaveChangesAsync();
            return photo;
        }

        public Task<Photo?> GetPhotoByIdAsync(int id)
        {
            return Task.FromResult(_photoContext.Photos.FirstOrDefault(x => x.Id == id));
        }

        public async Task RemovePhotoAsync(int id)
        {
            _photoContext.Photos.Remove(await GetPhotoByIdAsync(id));
            _photoContext.SaveChangesAsync();
        }

        public async Task UpdatePhotoAsync(Photo photo)
        {
            Photo? photoToUpdate = await GetPhotoByIdAsync(photo.Id);
            if (photoToUpdate is null)
                return;

            photoToUpdate.Title = photo.Title ?? photoToUpdate.Title;
            photoToUpdate.Description = photo.Description ?? photoToUpdate.Description;
            photoToUpdate.PhotoFile = photo.PhotoFile ?? photoToUpdate.PhotoFile;
            photoToUpdate.ImageMimeType = photo.ImageMimeType ?? photoToUpdate.ImageMimeType;

            _photoContext?.SaveChangesAsync();
        }
    }
}
