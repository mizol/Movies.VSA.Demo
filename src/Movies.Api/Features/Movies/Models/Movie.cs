namespace Movies.Api.Features.Movies.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Description { get; set; }
        public float AverageRating { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
        public ICollection<MovieRating> MovieRatings { get; set; }
    }

    public class MovieDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Description { get; set; }
        public List<GenreDto> Genres { get; set; }
    }

    public class GenreDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

}
