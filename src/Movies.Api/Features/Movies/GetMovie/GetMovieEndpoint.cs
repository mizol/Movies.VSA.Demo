﻿using Carter;
using Common.Core;
using MediatR;
using Movies.Api.Extensions;
using Movies.Api.Features.Movies.Models;

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
                    : Results.NotFound(result.GetProblemDetails(StatusCodes.Status404NotFound));
            })
            .WithName("GetMovie")
            .Produces(StatusCodes.Status404NotFound)
            .Produces<Result<MovieDto>>(StatusCodes.Status200OK);
        }
    }
}
