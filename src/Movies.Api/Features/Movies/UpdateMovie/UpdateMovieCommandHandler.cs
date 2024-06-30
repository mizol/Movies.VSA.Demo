using Common.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Api.Data;
using Movies.Api.Entities;
using Movies.Api.Features.Movies.Validation;

namespace Movies.Api.Features.Movies.UpdateMovie
{
    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, Result>
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidator<UpdateMovieCommand> _validator;

        public UpdateMovieCommandHandler(ApplicationDbContext context, IValidator<UpdateMovieCommand> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<Result> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return Result.Failure<Guid>(new Error(
                    "UpdateMovie.Validation",
                    validationResult.ToString()));
            }

            var movie = await _context.Movies
                .Include(m => m.MovieGenres)
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            if (movie == null)
            {
                return Result.Failure(MovieErrors.MovieNotFound(request.Id));
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
