// Endpoints/MoviesEndpoints.cs
using Common.Core;
using Mapster;
using MediatR;
using Movies.Api.Features.Movies.CreateMovie;
using Movies.Api.Features.Movies.DeleteMovie;
using Movies.Api.Features.Movies.GetMovie;
using Movies.Api.Features.Movies.Models;
using Movies.Api.Features.Movies.RateMovie;
using Movies.Api.Features.Movies.SearchMovies;
using Movies.Api.Features.Movies.UpdateMovie;
using Movies.Api.Requests;

namespace Movies.Api.Endpoints
{
    public static class MoviesEndpoints
    {
        public static void MapMovieEndpoints(this IEndpointRouteBuilder app)
        {
            // CreateMovie
            app.MapPost("/movies", async (CreateMovieRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateMovieCommand>();
                var result = await sender.Send(command);
                return result.IsSuccess 
                    ? Results.Ok(result.Value) 
                    : Results.BadRequest(result.Errors);
            })
            .WithName("CreateMovie")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<Result<Guid>>(StatusCodes.Status200OK);

            // GetMovie by Id
            app.MapGet("/movies/{id:guid}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetMovieQuery(id));
                return result.IsSuccess 
                    ? Results.Ok(result.Value) 
                    : Results.NotFound(result.Errors);
            })
            .WithName("GetMovie")
            .Produces(StatusCodes.Status404NotFound)
            .Produces<Result<Movie>>(StatusCodes.Status200OK);

            // UpdateMovie
            app.MapPut("/movies/{id:guid}", async (Guid id, UpdateMovieCommand command, ISender sender) =>
            {
                if (id != command.Id) return Results.BadRequest("ID mismatch");
                var result = await sender.Send(command);
                return result.IsSuccess 
                    ? Results.Ok() 
                    : Results.BadRequest(result.Errors);
            })
            .WithName("UpdateMovie")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status200OK);

            // DeleteMovie
            app.MapDelete("/movies/{id:guid}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteMovieCommand(id));
                return result.IsSuccess 
                    ? Results.Ok() 
                    : Results.NotFound(result.Errors);
            })
            .WithName("DeleteMovie")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status200OK);

            // RateMovie
            app.MapPost("/movies/{id:guid}/rate", async (Guid id, RateMovieCommand command, ISender sender) =>
            {
                if (id != command.MovieId) return Results.BadRequest("ID mismatch");
                var result = await sender.Send(command);
                return result.IsSuccess 
                    ? Results.Ok() 
                    : Results.BadRequest(result.Errors);
            })
            .WithName("RateMovie")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status200OK);

            // SearchMovies
            app.MapGet("/movies/search", async (string title, int? releaseYear, string genre, ISender sender) =>
            {
                var result = await sender.Send(new SearchMoviesQuery(title, releaseYear, genre));
                return result.IsSuccess 
                    ? Results.Ok(result.Value) 
                    : Results.BadRequest(result.Errors);
            })
            .WithName("SearchMovies")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<Result<List<Movie>>>(StatusCodes.Status200OK);
        }
    }

}
