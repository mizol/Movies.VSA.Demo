// Features/Movies/SearchMovies/SearchMoviesQuery.cs
using Common.Core;
using MediatR;
using Movies.Api.Features.Movies.Models;

namespace Movies.Api.Features.Movies.SearchMovies
{
    public record SearchMoviesQuery(string Title, int? ReleaseYear, string Genre) : IRequest<Result<List<Movie>>>;
}
