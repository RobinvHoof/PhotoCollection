using BlazorApp.Client.Core.Interfaces;
using BlazorApp.Client.Core.Models;

namespace BlazorApp.Client.Infrastructure.Repositories
{
    public class DudCommentsRepository : ICommentsRepository
    {
        public Task<Comment> AddCommentAsync(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCommentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Comment?> GetCommentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetCommentsForPhotoAsync(int photoId)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> UpdateCommentAsync(Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}
