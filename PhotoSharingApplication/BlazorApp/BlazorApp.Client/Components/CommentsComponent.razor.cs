using BlazorApp.Client.Core.Interfaces;
using BlazorApp.Client.Core.Models;
using BlazorApp.Client.Infrastructure.Repositories;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Components
{
    public partial class CommentsComponent : ComponentBase
    {
        [Inject] public ICommentsRepository _commentsRepository { get; set; }

        [Parameter] public int PhotoId { get; set; }
        private List<Comment> comments = default!;

        private Comment newComment;

        protected override async Task OnInitializedAsync()
        {
            newComment = new Comment() { PhotoId = PhotoId };
            comments = await _commentsRepository.GetCommentsForPhotoAsync(PhotoId);
        }

        async Task AddComment(Comment comment)
        {
            await _commentsRepository.AddCommentAsync(comment);
            comments = await _commentsRepository.GetCommentsForPhotoAsync(PhotoId);
            newComment = new Comment() { PhotoId = PhotoId };
        }
        async Task UpdateComment(Comment comment)
        {
            await _commentsRepository.UpdateCommentAsync(comment);
            comments = await _commentsRepository.GetCommentsForPhotoAsync(PhotoId);
        }
        async Task DeleteComment(Comment comment)
        {
            await _commentsRepository.DeleteCommentAsync(comment.Id);
            comments = await _commentsRepository.GetCommentsForPhotoAsync(PhotoId);
        }
    }
}
