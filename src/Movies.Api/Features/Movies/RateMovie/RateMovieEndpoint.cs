using Carter;
using Mapster;
using MediatR;
using Movies.Api.Extensions;
using Movies.Api.Features.Movies.RateMovie;
using Movies.Api.Requests;

namespace Movies.Api.Features.Movies.GetMovie
{
    public class RateMovieEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/movies/{id:guid}/rate", async (Guid id, RateMovieRequest request, ISender sender) =>
            {
                var command = request.Adapt<RateMovieCommand>();

                if (id != command.MovieId)
                {
                    return Results.BadRequest("ID mismatch");
                }

                var result = await sender.Send(command);
                return result.IsSuccess
                    ? Results.Ok()
                    : Results.BadRequest(result.GetProblemDetails(StatusCodes.Status400BadRequest));
            })
            .WithName("RateMovie")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status200OK);
        }
    }
}
