// Features/Movies/GetMovie/GetMovieQueryHandler.cs
using Common.Core;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Api.Contracts.Responses;
using Movies.Api.Data;

namespace Movies.Api.Features.Movies.GetMovie
{
    public class GetMovieQueryHandler : IRequestHandler<GetMovieQuery, Result<MovieDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetMovieQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<MovieDto>> Handle(GetMovieQuery request, CancellationToken cancellationToken)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieRatings)
                .AsSplitQuery()
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            if (movie == null)
            {
                return Result<MovieDto>.Failure(new Error("Movie.NotFound", $"Movie not found by if: {request.Id}."));
            }

            var movieDto = movie.Adapt<MovieDto>();

            return Result<MovieDto>.Success(movieDto);
        }
    }
}
