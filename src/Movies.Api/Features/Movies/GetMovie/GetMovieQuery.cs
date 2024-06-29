// Features/Movies/GetMovie/GetMovieQuery.cs
using Common.Core;
using MediatR;
using Movies.Api.Contracts.Responses;

namespace Movies.Api.Features.Movies.GetMovie
{
    public record GetMovieQuery(Guid Id) : IRequest<Result<MovieDto>>;
}
