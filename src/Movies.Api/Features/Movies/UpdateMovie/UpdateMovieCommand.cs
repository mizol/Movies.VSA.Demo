// Features/Movies/UpdateMovie/UpdateMovieCommand.cs
using Common.Core;
using MediatR;

namespace Movies.Api.Features.Movies.UpdateMovie
{
    public record UpdateMovieCommand(
        Guid Id,
        string Title,
        int ReleaseYear,
        string Description,
        List<Guid> GenreIds) : IRequest<Result>;
}
