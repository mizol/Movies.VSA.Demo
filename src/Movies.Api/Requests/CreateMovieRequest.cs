namespace Movies.Api.Requests
{
    public record CreateMovieRequest(
        string Title,
        int ReleaseYear,
        string Description,
        List<Guid> GenreIds);          
}
