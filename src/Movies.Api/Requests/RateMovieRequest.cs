namespace Movies.Api.Requests
{
    public record RateMovieRequest(Guid MovieId, int Rating);
}
