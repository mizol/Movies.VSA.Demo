// Movies.Api.Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using Movies.Api.Features.Movies.Models;

namespace Movies.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        {
            //
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(mg => mg.MovieId);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => mg.GenreId);

            // Adding dummy data
            SeedInitialData(modelBuilder);
        }

        private static void SeedInitialData(ModelBuilder modelBuilder)
        {
            var genres = new List<Genre>
            {
                new Genre { Id = Guid.NewGuid(), Name = "Action" },
                new Genre { Id = Guid.NewGuid(), Name = "Comedy" },
                new Genre { Id = Guid.NewGuid(), Name = "Drama" },
                new Genre { Id = Guid.NewGuid(), Name = "Sci-Fi" }
            };

            var movies = new List<Movie>
            {
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "Inception",
                    ReleaseYear = 2010,
                    Description = "A thief who steals corporate secrets through the use of dream-sharing technology...",
                    AverageRating = 4.5f
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "The Matrix",
                    ReleaseYear = 1999,
                    Description = "A computer hacker learns from mysterious rebels about the true nature of his reality...",
                    AverageRating = 4.8f
                }
            };

            var movieRatings = new List<MovieRating>
            {
                new MovieRating { Id = 1, MovieId = movies[0].Id, Rating = 5 },
                new MovieRating { Id = 2, MovieId = movies[0].Id, Rating = 4 },
                new MovieRating { Id = 3, MovieId = movies[1].Id, Rating = 5 }
            };

            var movieGenres = new List<MovieGenre>
            {
                new MovieGenre { MovieId = movies[0].Id, GenreId = genres[0].Id }, // Inception - Action
                new MovieGenre { MovieId = movies[0].Id, GenreId = genres[3].Id }, // Inception - Sci-Fi
                new MovieGenre { MovieId = movies[1].Id, GenreId = genres[0].Id }, // The Matrix - Action
                new MovieGenre { MovieId = movies[1].Id, GenreId = genres[3].Id }  // The Matrix - Sci-Fi
            };

            modelBuilder.Entity<Genre>().HasData(genres);
            modelBuilder.Entity<Movie>().HasData(movies);
            modelBuilder.Entity<MovieRating>().HasData(movieRatings);
            modelBuilder.Entity<MovieGenre>().HasData(movieGenres);
        }
    }
}
