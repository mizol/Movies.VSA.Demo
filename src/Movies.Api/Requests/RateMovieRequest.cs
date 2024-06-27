namespace Movies.Api.Requests
{
    public class RateMovieRequest {
        public Guid MovieId { get; set; }
        public int Rating { get; set; }
    }
}
