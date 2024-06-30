using Common.Core;
using Microsoft.AspNetCore.Mvc;

namespace Movies.Api.Extensions
{
    public static class ResultExtentions
    {
        public static ProblemDetails MapToProblemDetails(this Result result, int statusCode)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            if (result.Errors.Count == 1)
            {
                var error = result.Errors[0];
                return new ProblemDetails
                {
                    Status = statusCode,
                    Title = error.Code,
                    Detail = error.Description
                };
            }
            else
            {
                return new ProblemDetails
                {
                    Status = statusCode,
                    Title = "One or more errors occurred.",
                    Detail = string.Join("; ", result.Errors.Select(e => e.Description))
                };
            }
        }
    }
}
