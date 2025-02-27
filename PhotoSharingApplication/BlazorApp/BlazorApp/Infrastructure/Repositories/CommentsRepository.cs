using BlazorApp.Core.Interfaces;
using BlazorApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Infrastructure.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly PhotoContext _photoContext;
        public CommentsRepository(PhotoContext context) 
        { 
            _photoContext = context;
        }

        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            _photoContext.Comments.Add(comment);
            await _photoContext.SaveChangesAsync();
            return comment;
        }

        public async Task DeleteCommentAsync(int id)
        {
            _photoContext.Comments.Remove(await GetCommentByIdAsync(id));
            await _photoContext.SaveChangesAsync();
        }

        public Task<Comment?> GetCommentByIdAsync(int id)
        {
            return Task.FromResult(_photoContext.Comments.FirstOrDefault(c => c.Id == id));
        }

        public async Task<List<Comment>> GetCommentsForPhotoAsync(int photoId)
        {
            return await _photoContext.Comments.Where(c => c.PhotoId == photoId).ToListAsync();
        }

        public async Task<Comment> UpdateCommentAsync(Comment comment)
        {
            Comment? _comment = await GetCommentByIdAsync(comment.Id);

            _comment.PhotoId = comment.PhotoId;
            _comment.Body = comment.Body;
            _comment.Subject = comment.Subject;

            return _comment;
        }
    }
}
