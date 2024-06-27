namespace Movies.Api.Requests
{
    public class UpdateMovieRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Description { get; set; }
        public List<Guid> GenreIds { get; set; }
    }
}
