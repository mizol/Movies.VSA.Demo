using Common.Core;
using MediatR;

namespace Movies.Api.Features.Movies.DeleteMovie
{
    public record DeleteMovieCommand(Guid Id) : IRequest<Result>;
}
