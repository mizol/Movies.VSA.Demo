// Features/Movies/GetMovie/GetMovieQueryHandler.cs
using Common.Core;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Api.Data;
using Movies.Api.Features.Movies.Models;

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
            //movieDto.Genres =
            //var movieDto = new MovieDto
            //{
            //    Id = movie.Id,
            //    Title = movie.Title,
            //    ReleaseYear = movie.ReleaseYear,
            //    Description = movie.Description,
            //    Genres = movie.MovieGenres.Select(mg => new GenreDto { Id = mg.Genre.Id, Name = mg.Genre.Name }).ToList()
            //};

            return Result<MovieDto>.Success(movieDto);
        }
    }
}
