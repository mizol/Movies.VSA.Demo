using Common.Core;
using MediatR;
using Movies.Api.Contracts.Responses;

namespace Movies.Api.Features.Movies.SearchMovies
{
    public record SearchMoviesQuery(string Title, int? ReleaseYear, string Genre) : IRequest<Result<List<MovieDto>>>;
}
