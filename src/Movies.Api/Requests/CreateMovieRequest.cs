namespace Movies.Api.Requests
{
    public class CreateMovieRequest
    {
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Description { get; set; }
        public List<Guid> GenreIds { get; set; }
    }
}
