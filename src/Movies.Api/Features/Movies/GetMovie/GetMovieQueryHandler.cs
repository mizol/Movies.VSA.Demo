// Features/Movies/GetMovie/GetMovieQueryHandler.cs
using Common.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Api.Data;
using Movies.Api.Features.Movies.Models;

namespace Movies.Api.Features.Movies.GetMovie
{
    public class GetMovieQueryHandler : IRequestHandler<GetMovieQuery, Result<Movie>>
    {
        private readonly ApplicationDbContext _context;

        public GetMovieQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Movie>> Handle(GetMovieQuery request, CancellationToken cancellationToken)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieRatings)
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            if (movie == null)
            {
                return Result<Movie>.Failure(movie, new Error("NotFound", "Movie not found."));
            }

            return Result<Movie>.Success(movie);
        }
    }
}
