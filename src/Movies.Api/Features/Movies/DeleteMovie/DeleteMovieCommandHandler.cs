using Common.Core;
using MediatR;
using Movies.Api.Data;
using Movies.Api.Features.Movies.Validation;

namespace Movies.Api.Features.Movies.DeleteMovie
{
    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, Result>
    {
        private readonly ApplicationDbContext _context;

        public DeleteMovieCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _context.Movies.FindAsync(new object[] { request.Id }, cancellationToken);
            
            if (movie == null)
            {
                return Result.Failure(MovieErrors.MovieNotFound(request.Id));
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
