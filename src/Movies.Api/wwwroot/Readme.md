
# Welcome to the Movies API!

It's a pet project to learn minimal-api and Verstical Slice Architecture (VSA).
I'd like to document me learning path for the minimal API concept.
In addition to the VSA I'd like to hightlight some extra aspects that I faced with.


### REPR Pattern

The REPR Design Pattern defines web API endpoints as having three components: a Request, an Endpoint, and a Response. It simplifies the frequently-used MVC pattern and is more focused on API development.

- [REPR Pattern - Chaitanya (Chey) Penmetsa](https://medium.com/codenx/repr-pattern-endpoints-in-net-8-013fff3e8cfa)

### Verstical Slice Architecture (VSA)

Vertical Slice Architecture was born from the pain of working with layered architectures. They force you to make changes in many different layers to implement a feature.

- [Vertical Slice Architecture - Milan Jovanović](https://www.milanjovanovic.tech/blog/vertical-slice-architecture)

- (Youtube) [Vertical Slice Architecture Project Setup From Scratch - Milan Jovanović](https://youtu.be/msjnfdeDCmo?si=tt7k_vb2R6-zWi7O)

- (Youtube) [Getting Started With MediatR and Vertical Slices in .NET - Nick Chapsas](https://youtu.be/Ve__md8LeDY?si=QgOsR294epD9Z5tp)


### CQRS with MediatR

CQRS stands for Command Query Responsibility Segregation. The CQRS pattern uses separate models for reading and updating data. The benefits of using CQRS are complexity management, improved performance, scalability, and security.

- [CQRS Pattern with MediatR - Milan Jovanović](https://www.milanjovanovic.tech/blog/cqrs-pattern-with-mediatr)


### Result Pattern

The Result pattern is a way to handle operations that can succeed or fail, providing more structure and clarity compared to traditional error handling.

- [Functional Error Handling in .NET With the Result Pattern - Milan Jovanović](https://www.milanjovanovic.tech/blog/functional-error-handling-in-dotnet-with-the-result-pattern)


### ProblemDetails

A machine-readable format for specifying errors in HTTP API responses based on.

- [Problem Details MS Doc](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.problemdetails?view=aspnetcore-8.0)

Example:
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

### GlobalExceptionHandler .NET8 and IExceptionHandler

Global Exception Handler it's a place to log and handle unknown errors.

- [Global Error Handling in ASP.NET Core 8 - Milan Jovanović](https://www.milanjovanovic.tech/blog/global-error-handling-in-aspnetcore-8)


### Carter nuget

Carter is a framework that is a thin layer of extension methods and functionality over ASP.NET Core. 
It simplifies the process of defining and organizing endpoints in your application.

- [Carter Community](https://github.com/CarterCommunity/Carter/blob/main/README.md)

### Mapping Contracts and endpoint handlers to decouple layers.

- [Mapster nuget](https://github.com/MapsterMapper/Mapster/blob/master/README.md)

 
### FluentValidation and MediatR pipeline

- [CQRS Validation with MediatR Pipeline and FluentValidation](https://www.milanjovanovic.tech/blog/cqrs-validation-with-mediatr-pipeline-and-fluentvalidation)


### Architecture tests

- [Enforcing Software Architecture With Architecture Tests](https://www.milanjovanovic.tech/blog/enforcing-software-architecture-with-architecture-tests)

### Solution structure

```plaintext
Movies.Api/
├── 📁Behaviors/
│   └── ValidationBehavior.cs
├── 📁Configuration/
│   └── MapsterConfiguration.cs
├── 📁Contracts/
│   ├── 📁Requests/
│   │   ├── CreateMovieRequest.cs
│   │   ├── RateMovieRequest.cs
│   │   └── UpdateMovieRequest.cs
│   ├── 📁Responses/
│   │   ├── GenreDto.cs
│   │   └── MovieDto.cs
├── 📁Data/
│   ├── 📁Migrations/
│   └── ApplicationDbContext.cs
├── 📁Entities/
│   ├── Genre.cs
│   ├── Movie.cs
│   ├── MovieGenre.cs
│   └── MovieRating.cs
├── 📁Extensions/
│   └── ResultExtensions.cs
├── 📁Features/
│   ├── 📁Movies/
│   │   ├── 📁CreateMovie/
│   │   │   ├── CreateMovieCommand.cs
│   │   │   ├── CreateMovieCommandHandler.cs
│   │   │   ├── CreateMovieEndpoint.cs
│   │   │   └── CreateMovieValidator.cs
│   │   ├── 📁GetMovie/
│   │   │   ├── GetMovieEndpoint.cs
│   │   │   ├── GetMovieQuery.cs
│   │   │   └── GetMovieQueryHandler.cs
│   │   ├── 📁UpdateMovie/
│   │   │   ├── UpdateMovieCommand.cs
│   │   │   ├── UpdateMovieCommandHandler.cs
│   │   │   ├── UpdateMovieEndpoint.cs
│   │   │   └── UpdateMovieValidator.cs
│   │   ├── 📁DeleteMovie/
│   │   │   ├── DeleteMovieCommand.cs
│   │   │   ├── DeleteMovieCommandHandler.cs
│   │   │   └── DeleteMovieEndpoint.cs
│   │   ├── 📁RateMovie/
│   │   │   ├── RateMovieCommand.cs
│   │   │   ├── RateMovieCommandHandler.cs
│   │   │   ├── RateMovieEndpoint.cs
│   │   │   └── RateMovieValidator.cs
│   │   ├── 📁SearchMovies/
│   │   │   ├── SearchMoviesQuery.cs
│   │   │   ├── SearchMoviesQueryHandler.cs
│   │   │   └── SearchMovieEndpoint.cs
├── 📁Middleware/
│   └── GlobalExceptionHandler.cs
├── 📁Services/
│   └── DateTimeProvider.cs

```

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

#### References:
 - [Minimal APIs Overview](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/overview?view=aspnetcore-8.0)
 

 - ASP.NET-Core-Vertical-Slice-Architecture

 https://github.com/jeangatto

 https://github.com/jeangatto/ASP.NET-Core-Vertical-Slice-Architecture/tree/main/src/Blog.PublicAPI


 - Nadir Badnjevic

  https://nadirbad.dev/vertical-slice-architecture-dotnet

  https://github.com/nadirbad/VerticalSliceArchitecture


- Code Style

  https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/code-style-rule-options?view=vs-2019