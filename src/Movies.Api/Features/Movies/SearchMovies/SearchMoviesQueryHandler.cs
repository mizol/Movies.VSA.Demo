﻿using Common.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
// Features/Movies/SearchMovies/SearchMoviesQueryHandler.cs
using Movies.Api.Data;
using Movies.Api.Features.Movies.Models;

namespace Movies.Api.Features.Movies.SearchMovies
{
    public class SearchMoviesQueryHandler : IRequestHandler<SearchMoviesQuery, Result<List<Movie>>>
    {
        private readonly ApplicationDbContext _context;

        public SearchMoviesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<Movie>>> Handle(SearchMoviesQuery request, CancellationToken cancellationToken)
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

            var movies = await query
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieRatings)
                .ToListAsync(cancellationToken);

            return Result<List<Movie>>.Success(movies);
        }
    }

}



