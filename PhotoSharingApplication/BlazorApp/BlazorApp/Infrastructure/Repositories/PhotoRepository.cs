using BlazorApp.Core.Interfaces;
using BlazorApp.Core.Models;

namespace BlazorApp.Infrastructure.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private List<Photo> s_Photos;

        public PhotoRepository()
        {
            s_Photos = new List<Photo>{
                new Photo { Id = 1, Title = "My Title", Description = "Lorem ipsum dolor sit amen" },
                new Photo { Id = 2, Title = "Another Title", Description = "All work and no play makes Jack a dull boy" },
                new Photo { Id = 3, Title = "Yet another Title", Description = "Some description" }
            };
        }

        public Task<IEnumerable<Photo>> GetAllAsync()
        {
            return Task.FromResult(s_Photos.AsEnumerable());
        }

        public Task<Photo> AddAsync(Photo photo)
        {
            photo.Id = s_Photos.Max(x => x.Id) + 1;
            s_Photos.Add(photo);
            return Task.FromResult(photo);
        }

        public Task<Photo?> GetPhotoByIdAsync(int id)
        {
            return Task.FromResult(s_Photos.FirstOrDefault(x => x.Id == id));
        }

        public async Task RemovePhotoAsync(int id)
        {
            s_Photos.Remove(await GetPhotoByIdAsync(id));
        }

        public Task UpdatePhotoAsync(Photo photo)
        {
            Photo photoToUpdate = s_Photos.FirstOrDefault(p => p.Id == photo.Id);
            if (photoToUpdate is null)
                return Task.CompletedTask;

            photoToUpdate.Title = photo.Title;
            photoToUpdate.Description = photo.Description;
            photoToUpdate.PhotoFile = photo.PhotoFile;
            photoToUpdate.ImageMimeType = photo.ImageMimeType;

            return Task.CompletedTask;
        }
    }
}
