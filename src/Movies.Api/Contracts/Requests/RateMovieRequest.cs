namespace Movies.Api.Contracts.Requests
{
    public record RateMovieRequest(Guid MovieId, int Rating);
}
