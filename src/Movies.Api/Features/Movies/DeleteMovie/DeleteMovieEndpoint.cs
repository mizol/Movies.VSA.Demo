using Carter;
using MediatR;
using Movies.Api.Extensions;

namespace Movies.Api.Features.Movies.DeleteMovie
{
    public class DeleteMovieEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/movies/{id:guid}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteMovieCommand(id));
                return result.IsSuccess
                    ? Results.Ok()
                    : Results.NotFound(result.GetProblemDetails(StatusCodes.Status404NotFound));
            })
            .WithName("DeleteMovie")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status200OK);
        }
    }
}
