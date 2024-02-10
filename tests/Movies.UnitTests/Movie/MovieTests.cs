using AutoFixture;
using AutoMapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.Options;
using Moq;
using Movies.Application.Features.Movie.Queries.GetByImdbId;
using Movies.Application.Features.Movie.Queries.Search;
using Movies.Application.Mappings;
using Movies.Domain.Models;
using Movies.Domain.Services;
using Movies.Domain.Services.Common;
using Movies.Infrastructure.Services;
using MovieModel = Movies.Domain.Models.Movie;

namespace Movies.UnitTests.Movie;
public class MovieTests
{
    private readonly Mock<IMovieService> _movieService;
    private readonly Mock<IHttpClient> _httpClient;
    private readonly IMapper _mapper;
    private readonly Fixture _fixture;
    public MovieTests()
    {
        _fixture = new Fixture();
        _movieService = new Mock<IMovieService>();
        _httpClient = new Mock<IHttpClient>();

        var mockMapper = new MapperConfiguration(mc =>
        {
            mc.AddMaps(typeof(MovieMapping).Assembly);
        }).CreateMapper().ConfigurationProvider;

        _mapper = new Mapper(mockMapper);
    }

    [Fact]
    public async Task SearchMovies_WithValidSearchTerm_ReturnsOkResult()
    {
        var movies = _fixture.CreateMany<MovieModel>(10)
            .ToList();

        string searchTerm = It.IsAny<string>();

        var movieSearchResult = new MovieSearchResult
        {
            Response = "True",
            Search = movies,
            TotalResults = movies.Count.ToString()
        };

        _movieService.Setup(mvs => mvs.Search(searchTerm))
            .ReturnsAsync(movieSearchResult);

        var request = new SearchMovieRequest(searchTerm);

        var handler = new SearchMovieRequestHandler(_movieService.Object, _mapper);

        var result = await handler.Handle(request, default);

        using var _ = new AssertionScope();

        result.IsSuccess.Should().BeTrue();

        result.Value.Movies.Should().BeOfType<List<MovieResponse>>();

        result.Value.Should().BeOfType<SearchMovieResponse>();

        result.Value.TotalRecords.Should().Be(result.Value.Movies.Count);
    }

    [Fact]
    public async Task GetMovie_WithValidImdbId_ReturnsMovie()
    {
        var movie = _fixture.Create<MovieItemSearchResult>();

        _movieService.Setup(mvs => mvs.GetMovieByImdbId(It.IsAny<string>()))
            .ReturnsAsync(movie);

        var request = new GetMovieByImdbIdRequest(It.IsAny<string>());

        var handler = new GetMovieByImdbIdRequestHandler(_movieService.Object, _mapper);

        var result = await handler.Handle(request, default);

        using var _ = new AssertionScope();

        result.Should().NotBeNull();

        result.Value.Should().NotBeNull();

        result.Value.Should().BeOfType<GetMovieResponse>();
               
    }

    [Fact]
    public async Task GetMovie_WithIncorrectImdbId_ReturnsError()
    {
        var movieResult = _fixture.Create<MovieItemSearchResult>();

        movieResult.Response = "False";

        _httpClient.Setup(hc => hc.SendAsync<MovieItemSearchResult, MovieSearchErrorResult>(
            It.IsAny<Movies.Domain.Shared.HttpRequestMessage>()))
            .ReturnsAsync(movieResult);

        var optionsImdbCreds = new Mock<IOptions<ImdbCredentials>>();

        optionsImdbCreds.Setup(op => op.Value)
            .Returns(new ImdbCredentials
            {
                ApiKey = "",
                BaseUrl = ""
            });

        var movieService = new MovieService(_httpClient.Object, optionsImdbCreds.Object);

        var result = await movieService.GetMovieByImdbId(It.IsAny<string>());

        using var _ = new AssertionScope();

        result.IsFailure.Should().BeTrue();

        result.Error.Should().NotBeNull();
    }
}
