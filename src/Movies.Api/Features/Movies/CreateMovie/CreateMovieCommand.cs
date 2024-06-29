// Features/Movies/CreateMovie/CreateMovieCommand.cs
using Common.Core;
using MediatR;

namespace Movies.Api.Features.Movies.CreateMovie
{
    public record CreateMovieCommand(
        string Title,
        int ReleaseYear,
        string Description,
        List<Guid> GenreIds) : IRequest<Result<Guid>>;
}
