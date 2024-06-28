// Features/Movies/SearchMovies/SearchMoviesQueryHandler.cs
using Common.Core;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Api.Data;
using Movies.Api.Features.Movies.Models;

namespace Movies.Api.Features.Movies.SearchMovies
{
    public class SearchMoviesQueryHandler : IRequestHandler<SearchMoviesQuery, Result<List<MovieDto>>>
    {
        private readonly ApplicationDbContext _context;

        public SearchMoviesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<MovieDto>>> Handle(SearchMoviesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(request.Title))
            {
                query = query.Where(m => m.Title.Contains(request.Title));
            }

            if (request.ReleaseYear.HasValue)
            {
                query = query.Where(m => m.ReleaseYear == request.ReleaseYear.Value);
            }

            if (!string.IsNullOrEmpty(request.Genre))
            {
                query = query.Where(m => m.MovieGenres.Any(mg => mg.Genre.Name == request.Genre));
            }

            // pagination

            var movies = await query
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieRatings)
                .AsSplitQuery()
                .ToListAsync(cancellationToken);

            var movieDtoList = movies.Adapt<List<MovieDto>>();
            return Result<List<MovieDto>>.Success(movieDtoList);
        }
    }

}



