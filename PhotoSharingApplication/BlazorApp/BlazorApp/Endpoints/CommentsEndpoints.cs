using BlazorApp.Core.Interfaces;
using BlazorApp.Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BlazorApp.Endpoints
{
    public static class CommentsEndpoints
    {
        public static IEndpointRouteBuilder MapCommentsEndpoints(this IEndpointRouteBuilder builder)
        {
            RouteGroupBuilder commentsGroup = builder.MapGroup("/api/comments")
                .WithTags("Comments");
            RouteGroupBuilder photoGroup = builder.MapGroup("/api/photos")
                .WithTags("Photos");
            
            commentsGroup.MapGet("/{id:int}", GetComment);
            commentsGroup.MapDelete("/{id:int}", DeleteComment);
            commentsGroup.MapPost("/", CreateComment);

            photoGroup.MapGet("/{photoId:int}/comments", GetCommentsForPhoto)
                .WithTags("Comments");

            return builder;
        }

        public static async Task<Results<NotFound, Ok<Comment>>> GetComment(
            ICommentsRepository commentsRepository,
            int id
        ){
            Comment? comment = await commentsRepository.GetCommentByIdAsync(id);

            if (comment == null )
                return TypedResults.NotFound();

            return TypedResults.Ok<Comment>(comment);
        }

        public static async Task<Ok<List<Comment>>> GetCommentsForPhoto(
            ICommentsRepository commentsRepository,
            int photoId
        ){
            List<Comment> comments = await commentsRepository.GetCommentsForPhotoAsync(photoId);
            return TypedResults.Ok(comments);
        }

        public static async Task<Results<NotFound, NoContent>> DeleteComment(
            ICommentsRepository commentsRepository,
            int id
        ){
            var comment = await commentsRepository.GetCommentByIdAsync(id);
            if (comment is null)
            {
                return TypedResults.NotFound();
            }

            await commentsRepository.DeleteCommentAsync(id);
            return TypedResults.NoContent();
        }

        public static async Task<Created<Comment>> CreateComment(
            ICommentsRepository commentsRepository,
            Comment comment
        ){
            Comment createdComment = await commentsRepository.AddCommentAsync(comment);
            return TypedResults.Created($"https://localhost:7182/api/comments/{createdComment.Id}", createdComment);
        }
    }
}
