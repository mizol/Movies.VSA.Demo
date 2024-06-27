using FluentValidation;

namespace Movies.Api.Features.Movies.RateMovie
{
    public class RateMovieValidator : AbstractValidator<RateMovieCommand>
    {
        public RateMovieValidator()
        {
            RuleFor(x => x.MovieId)
                .NotEmpty()
                .WithMessage("Movie Id must not be empty.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 10)
                .WithMessage("Rating must be between 1 and 10.");
        }
    }
}
