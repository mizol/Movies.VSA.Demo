using Carter;
using Mapster;
using MediatR;
using Movies.Api.Contracts.Requests;
using Movies.Api.Extensions;
using Movies.Api.Features.Movies.UpdateMovie;

namespace Movies.Api.Features.Movies.GetMovie
{
    public class UpdateMovieEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/movies/{id:guid}", async (Guid id, UpdateMovieRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateMovieCommand>();
                if (id != command.Id)
                {
                    return Results.BadRequest("ID mismatch");
                }

                var result = await sender.Send(command);
                return result.IsSuccess
                    ? Results.Ok()
                    : Results.BadRequest(result.MapToProblemDetails(StatusCodes.Status400BadRequest));
            })
            .WithName("UpdateMovie")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status200OK);
        }
    }
}
