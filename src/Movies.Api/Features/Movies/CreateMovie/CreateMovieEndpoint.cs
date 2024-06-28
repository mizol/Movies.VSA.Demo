using Carter;
using Common.Core;
using Mapster;
using MediatR;
using Movies.Api.Contracts.Requests;
using Movies.Api.Extensions;

namespace Movies.Api.Features.Movies.CreateMovie
{
    public class CreateMovieEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // CreateMovie
            app.MapPost("/movies", async (CreateMovieRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateMovieCommand>();
                var result = await sender.Send(command);

                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.BadRequest(result.GetProblemDetails(StatusCodes.Status400BadRequest));
            })
            .WithName("CreateMovie")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<Result<Guid>>(StatusCodes.Status200OK);
        }
    }
}
