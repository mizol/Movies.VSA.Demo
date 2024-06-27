// Features/Movies/RateMovie/RateMovieCommand.cs
using Common.Core;
using MediatR;

namespace Movies.Api.Features.Movies.RateMovie
{
    public record RateMovieCommand(Guid MovieId, int Rating) : IRequest<Result>;
}
