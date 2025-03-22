# Webjet Movies


### Setup

Follow these steps to get your development environment set up:

1. Clone the repository
2. At the root directory, restore required packages by running:

```bash
 dotnet restore
```

3. Next, build the solution by running:

```bash
dotnet build
```

5. Once the front end has started, within the `\Src\WebUI` directory, launch the back end by running:

```bash
dotnet run
```

6. Launch [https://localhost:44427/](https://localhost:44427/) in your browser to view the Web UI

7. Launch [https://localhost:44376/api](http://localhost:44376/api) in your browser to view the API

## Technologies

* .NET 8
* ASP.NET Core 8
* Entity Framework Core 8
* Angular 17

### Other Packages

* MediatR
* FluentValidation
* AutoMapper
* Ardalis.Specification
* Ardalis.GuardClauses

### Testing Packages

* xUnit
* NSubstitute
* TestContainers
* Fluent Assertions
* Respawn
* Bogus
