namespace Movies.Api.Features.Movies.Models
{
    public class MovieRating
    {
        public int Id { get; set; }
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        public int Rating { get; set; }
    }
}
