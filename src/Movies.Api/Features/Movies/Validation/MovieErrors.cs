using Common.Core;

namespace Movies.Api.Features.Movies.Validation
{
    public static class MovieErrors
    {
        public static Error MovieNotFound(Guid movieId) => new(
            "Movies.NotFound", $"The movie not found by id: '{movieId}'");
    }
}
