using Carter;
using Common.Core;
using MediatR;
using Movies.Api.Contracts.Responses;
using Movies.Api.Extensions;

namespace Movies.Api.Features.Movies.GetMovie
{
    public class GetMovieEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/movies/{id:guid}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetMovieQuery(id));
                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.NotFound(result.MapToProblemDetails(StatusCodes.Status404NotFound));
            })
            .WithName("GetMovie")
            .Produces(StatusCodes.Status404NotFound)
            .Produces<Result<MovieDto>>(StatusCodes.Status200OK);
        }
    }
}
