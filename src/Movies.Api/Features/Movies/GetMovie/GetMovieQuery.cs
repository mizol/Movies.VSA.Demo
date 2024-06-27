// Features/Movies/GetMovie/GetMovieQuery.cs
using Common.Core;
using MediatR;
using Movies.Api.Features.Movies.Models;

namespace Movies.Api.Features.Movies.GetMovie
{
    public record GetMovieQuery(Guid Id) : IRequest<Result<Movie>>;
}
