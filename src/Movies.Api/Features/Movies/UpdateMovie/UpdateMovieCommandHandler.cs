// Features/Movies/UpdateMovie/UpdateMovieCommandHandler.cs
using Common.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Api.Data;
using Movies.Api.Entities;

namespace Movies.Api.Features.Movies.UpdateMovie
{
    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, Result>
    {
        private readonly ApplicationDbContext _context;

        public UpdateMovieCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieGenres)
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            if (movie == null)
            {
                return Result.Failure(new Error("NotFound", "Movie not found."));
            }

            movie.Title = request.Title;
            movie.ReleaseYear = request.ReleaseYear;
            movie.Description = request.Description;

            // Update genres
            movie.MovieGenres.Clear();
            foreach (var genreId in request.GenreIds)
            {
                movie.MovieGenres.Add(new MovieGenre { MovieId = movie.Id, GenreId = genreId });
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
