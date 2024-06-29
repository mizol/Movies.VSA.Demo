namespace Movies.Api.Contracts.Responses
{
    public class MovieDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Description { get; set; }
        public List<GenreDto> Genres { get; set; }
        public float AverageRating { get; set; }
    }

}
