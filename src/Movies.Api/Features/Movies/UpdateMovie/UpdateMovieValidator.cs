// Features/Movies/CreateMovie/CreateMovieValidator.cs
using Common.Core;
using FluentValidation;

namespace Movies.Api.Features.Movies.UpdateMovie
{
    public sealed class UpdateMovieValidator : AbstractValidator<UpdateMovieCommand>
    {
        public UpdateMovieValidator(IDateTimeProvider dateTimeProvider)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .WithMessage("Movie ID is required.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(255)
                .WithMessage("Title is required and should not exceed 255 characters.");

            RuleFor(x => x.ReleaseYear)
                .GreaterThan(1800).WithMessage("Release year must be greater than 1800.")
                .LessThanOrEqualTo(dateTimeProvider.Now.Year).WithMessage($"Release year must not be in the future.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.");

            RuleFor(x => x.GenreIds)
                .NotEmpty()
                .WithMessage("At least one genre ID is required.");
        }
    }
}
