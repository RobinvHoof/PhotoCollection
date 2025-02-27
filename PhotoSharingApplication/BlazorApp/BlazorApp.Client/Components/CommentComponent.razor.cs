using BlazorApp.Client.Core.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Components
{
    public partial class CommentComponent : ComponentBase
    {
        // Comment Parameter
        [Parameter] public Comment Comment { get; set; }
        private Comment comment;

        // CommentState Parameter
        [Parameter] public CommentStates CommentState { get; set; } = CommentStates.View;
        private CommentStates commentState;

        // Callback Parameters
        [Parameter] public EventCallback<Comment> OnAddComment { get; set; }
        [Parameter] public EventCallback<Comment> OnUpdateComment { get; set; }
        [Parameter] public EventCallback<Comment> OnDeleteComment { get; set; }

        protected override void OnParametersSet()
        {
            commentState = CommentState;
            comment = new Comment()
            {
                Id = Comment.Id,
                PhotoId = Comment.PhotoId,
                Subject = Comment.Subject,
                Body = Comment.Body
            };
        }

        async Task OnAddClicked()
        {
            await OnAddComment.InvokeAsync(comment);
        }

        async Task OnSaveClicked()
        {
            commentState = CommentStates.View;
            await OnUpdateComment.InvokeAsync(comment);
        }

        async Task OnDeleteConfirmedClicked()
        {
            commentState = CommentStates.View;
            await OnDeleteComment.InvokeAsync(comment);
        }

        async Task OnDeleteClicked()
        {
            commentState = CommentStates.Delete;
        }

        async Task OnCancelClicked()
        {
            commentState = CommentStates.View;
        }

        public enum CommentStates
        {
            View,
            Insert,
            Edit,
            Delete
        }
    }
}
