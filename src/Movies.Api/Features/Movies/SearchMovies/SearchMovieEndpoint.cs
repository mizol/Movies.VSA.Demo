using Carter;
using Common.Core;
using Mapster;
using MediatR;
using Movies.Api.Features.Movies.Models;
using Movies.Api.Features.Movies.RateMovie;
using Movies.Api.Features.Movies.SearchMovies;
using Movies.Api.Requests;

namespace Movies.Api.Features.Movies.GetMovie
{
    public class SearchMovieEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
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
