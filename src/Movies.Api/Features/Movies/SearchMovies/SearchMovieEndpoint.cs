using Carter;
using Common.Core;
using MediatR;
using Movies.Api.Extensions;
using Movies.Api.Features.Movies.Models;
using Movies.Api.Features.Movies.SearchMovies;

namespace Movies.Api.Features.Movies.GetMovie
{
    public class SearchMovieEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/movies/search", async (string title, int? releaseYear, string? genre, ISender sender) =>
            {
                var result = await sender.Send(new SearchMoviesQuery(title, releaseYear, genre));
                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.BadRequest(result.GetProblemDetails(StatusCodes.Status400BadRequest));
            })
            .WithName("SearchMovies")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<Result<List<Movie>>>(StatusCodes.Status200OK);
        }
    }
}
