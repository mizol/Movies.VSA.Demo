// Features/Movies/CreateMovie/CreateMovieCommandHandler.cs
using MediatR;
using Movies.Api.Data;
using Movies.Api.Features.Movies.Models;
using Common.Core;

namespace Movies.Api.Features.Movies.CreateMovie
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;

        public CreateMovieCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
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
