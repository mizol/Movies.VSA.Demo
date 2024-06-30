using Common.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Api.Data;
using Movies.Api.Entities;
using Movies.Api.Features.Movies.Validation;

namespace Movies.Api.Features.Movies.RateMovie
{
    public class RateMovieCommandHandler : IRequestHandler<RateMovieCommand, Result>
    {
        private readonly ApplicationDbContext _context;

        public RateMovieCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(RateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieRatings)
                .FirstOrDefaultAsync(m => m.Id == request.MovieId, cancellationToken);

            if (movie == null)
            {
                return Result.Failure(MovieErrors.MovieNotFound(request.MovieId));
            }

            movie.MovieRatings.Add(new MovieRating { MovieId = request.MovieId, Rating = request.Rating });
            movie.AverageRating = (float)movie.MovieRatings.Average(mr => mr.Rating);

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
