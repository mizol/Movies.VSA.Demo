// Endpoints/MoviesEndpoints.cs
using Common.Core;
using MediatR;
using Movies.Api.Features.Movies.CreateMovie;

namespace Movies.Api.Endpoints
{
    public static class MoviesEndpoints
    {
        public static void MapMovieEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/movies", async (CreateMovieCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);
                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
            }).Produces<Result<Guid>>();

            //app.MapGet("/movies/{id:guid}", async (Guid id, ISender sender) =>
            //{
            //    var result = await sender.Send(new GetMovieQuery(id));
            //    return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.ErrorType);
            //}).Produces<Result<Movie>>();

            //app.MapPut("/movies/{id:guid}", async (Guid id, UpdateMovieCommand command, ISender sender) =>
            //{
            //    if (id != command.Id) return Results.BadRequest("ID mismatch");
            //    var result = await sender.Send(command);
            //    return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.ErrorType);
            //}).Produces<Result>();

            //app.MapDelete("/movies/{id:guid}", async (Guid id, ISender sender) =>
            //{
            //    var result = await sender.Send(new DeleteMovieCommand(id));
            //    return result.IsSuccess ? Results.Ok() : Results.NotFound(result.ErrorType);
            //}).Produces<Result>();

            //app.MapPost("/movies/{id:guid}/rate", async (Guid id, RateMovieCommand command, ISender sender) =>
            //{
            //    if (id != command.MovieId) return Results.BadRequest("ID mismatch");
            //    var result = await sender.Send(command);
            //    return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.ErrorType);
            //}).Produces<Result>();

            //app.MapGet("/movies/search", async (string title, int? releaseYear, string genre, ISender sender) =>
            //{
            //    var result = await sender.Send(new SearchMoviesQuery(title, releaseYear, genre));
            //    return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.ErrorType);
            //}).Produces<Result<List<Movie>>>();
        }
    }

}
