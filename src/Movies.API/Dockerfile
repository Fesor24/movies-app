FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY movies.sln movies.sln
COPY src/Movies.API/Movies.API.csproj src/Movies.API/Movies.API.csproj
COPY src/Movies.Application/Movies.Application.csproj src/Movies.Application/Movies.Application.csproj
COPY src/Movies.Domain/Movies.Domain.csproj src/Movies.Domain/Movies.Domain.csproj
COPY src/Movies.Infrastructure/Movies.Infrastructure.csproj src/Movies.Infrastructure/Movies.Infrastructure.csproj
COPY tests/Movies.UnitTests/Movies.UnitTests.csproj tests/Movies.UnitTests/Movies.UnitTests.csproj

WORKDIR /app/src/Movies.API
RUN dotnet restore

WORKDIR /app
COPY src/Movies.API src/Movies.API
COPY src/Movies.Application src/Movies.Application
COPY src/Movies.Infrastructure src/Movies.Infrastructure
COPY src/Movies.Domain src/Movies.Domain
COPY tests/Movies.UnitTests tests/Movies.UnitTests

WORKDIR /app/src/Movies.API
RUN dotnet publish -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Movies.API.dll"]