using Common.Core;
using FluentValidation;
using MediatR;

namespace Movies.Api.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse> 
        where TResponse : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;            
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    var error = new Error("ValidationError", string.Join("; ", failures.Select(f => f.ErrorMessage)));

                    var responseType = typeof(TResponse);

                    return (TResponse)responseType
                        .GetConstructors()[0]
                        .Invoke(new object[] { default, error });
                }
            }

            return await next();
        }
    }
}