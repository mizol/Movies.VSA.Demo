using Common.Core;
using FluentValidation;
using MediatR;
using Movies.Api.Data;
using Movies.Api.Entities;

namespace Movies.Api.Features.Movies.CreateMovie
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidator<CreateMovieCommand> _validator;

        public CreateMovieCommandHandler(ApplicationDbContext context, IValidator<CreateMovieCommand> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<Result<Guid>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return Result.Failure<Guid>(new Error(
                    "CreateMovie.Validation",
                    validationResult.ToString()));
            }

            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                ReleaseYear = request.ReleaseYear,
                Description = request.Description,
                MovieGenres = request.GenreIds.Select(id => new MovieGenre { GenreId = id }).ToList()
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(movie.Id);
        }
    }

}
