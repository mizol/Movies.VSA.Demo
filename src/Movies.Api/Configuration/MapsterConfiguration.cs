﻿namespace Movies.Api.Configuration
{
    using Mapster;
    using Movies.Api.Contracts.Responses;
    using Movies.Api.Entities;

    public static class MapsterConfiguration
    {
        public static void Configure()
        {
            // Movie.Genres
            TypeAdapterConfig<Movie, MovieDto>.NewConfig()
                .Map(dest => dest.Genres, src => src.MovieGenres.Select(mg => mg.Genre.Adapt<GenreDto>()));

            TypeAdapterConfig<MovieGenre, GenreDto>.NewConfig()
                .Map(dest => dest.Id, src => src.Genre.Id)
                .Map(dest => dest.Name, src => src.Genre.Name);
        }
    }
}
