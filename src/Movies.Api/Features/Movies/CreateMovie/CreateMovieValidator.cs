// Features/Movies/CreateMovie/CreateMovieValidator.cs
using FluentValidation;

namespace Movies.Api.Features.Movies.CreateMovie
{
    public class CreateMovieValidator : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
            RuleFor(x => x.ReleaseYear).GreaterThan(1800);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.GenreIds).NotEmpty();
        }
    }
}
