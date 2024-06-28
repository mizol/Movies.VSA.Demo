
## Welcome to the Movies API!
### Vertical Slice Architecture Project Setup From Scratch


It's a pet project to learn minimal-api and Verstical Slice Architecture (VSA).
I'd like to document me learning path for the minimal API concept.
In addition to the VSA I'd like to hightlight some extra aspects that I faced with.

I was inspired by this youtube channel: 
[Milan Jovanović](https://www.youtube.com/@MilanJovanovicTech)

Solution scheme:


### The project features under test

- Verstical Slice Architecture (VSA)

Nick Chapsas
https://youtu.be/Ve__md8LeDY?si=QgOsR294epD9Z5tp


- MediatR

- CQRS




- [Result Pattern - Milan](https://www.milanjovanovic.tech/blog/functional-error-handling-in-dotnet-with-the-result-pattern) The Result class implementation: Common.Core.Result.cs. 

The Result pattern is a way to handle operations that can succeed or fail, providing more structure and clarity compared to traditional error handling.

```C#
    app.MapDelete("/movies/{id:guid}", async (Guid id, ISender sender) =>
    {
        var result = await sender.Send(new DeleteMovieCommand(id));
        return result.IsSuccess
            ? Results.Ok()
            : Results.NotFound(result.GetProblemDetails(StatusCodes.Status404NotFound));
    })
    .WithName("DeleteMovie")
    .Produces(StatusCodes.Status404NotFound)
    .Produces(StatusCodes.Status200OK);
```

- ProblemDetails

A machine-readable format for specifying errors in HTTP API responses based on.

[Problem Details MS Doc](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.problemdetails?view=aspnetcore-8.0)

```C#
    [HttpDelete("{id}")]
    public ActionResult DeleteOrder(int id)
    {
        var order = Orders.FirstOrDefault(o => o.Id == id);
        if (order == null)
        {
            return NotFound(new ProblemDetails
            {
                Status = 404,
                Title = "Order not found",
                Detail = $"No order found with ID {id}"
            });
        }
        Orders.Remove(order);
        return NoContent();
    }
```

- GlobalExceptionHandler .NET8 and IExceptionHandler

[Exception handling middleware - Milan](https://www.milanjovanovic.tech/blog/global-error-handling-in-aspnetcore-8)


- Carter nuget
 // Map endpoints

- Mapping Contracts and endpoint handlers to decouple layers.
Mapster -> doc

 
- FluentValidation and MediatR pipeline:
 // Program.cs Register MediatR ValidationBehavior.cs



### What is the next
- GET Requests and Pagination
- API versioning
- Atuhentication and Authorization
- Cache strategy
- Rate Limiting
- EF Abstraction
- Dapper for query
- HATEOAS
- Health Checks https://youtu.be/p2faw9DCSsY?si=LjCPAhr1os0Czrtp

#### References
 - Youtube: https://youtu.be/msjnfdeDCmo?si=tt7k_vb2R6-zWi7O
 - [Minimal APIs Overview](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/overview?view=aspnetcore-8.0)
 - [Basic Authentication Tests](https://github.com/blowdart/idunno.Authentication/tree/dev/test/idunno.Authentication.Basic.Test)



 He

 https://github.com/jeangatto
 https://github.com/jeangatto/ASP.NET-Core-Vertical-Slice-Architecture/tree/main/src/Blog.PublicAPI


 Nadir Badnjevic

 - https://nadirbad.dev/vertical-slice-architecture-dotnet
 https://github.com/nadirbad/VerticalSliceArchitecture


 Code Style
 https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/code-style-rule-options?view=vs-2019