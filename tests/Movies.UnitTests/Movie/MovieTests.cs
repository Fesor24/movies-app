using AutoFixture;
using AutoMapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using Movies.Application.Features.Movie.Queries.GetByImdbId;
using Movies.Application.Features.Movie.Queries.Search;
using Movies.Application.Mappings;
using Movies.Application.Services;
using Movies.Domain.Models;
using Movies.Domain.Primitives;
using MovieModel = Movies.Domain.Models.Movie;

namespace Movies.UnitTests.Movie;
public class MovieTests
{
    private readonly Mock<IMovieService> _movieService;
    private readonly IMapper _mapper;
    private readonly Fixture _fixture;
    public MovieTests()
    {
        _fixture = new Fixture();
        _movieService = new Mock<IMovieService>();

        var mockMapper = new MapperConfiguration(mc =>
        {
            mc.AddMaps(typeof(MovieMapping).Assembly);
        }).CreateMapper().ConfigurationProvider;

        _mapper = new Mapper(mockMapper);
    }

    [Theory]
    [InlineData("spiderman")]
    [InlineData("superman")]
    public async Task SearchMovies_WithValidSearchTerm_ReturnsListOfMovies(string searchTerm)
    {
        var movies = _fixture.CreateMany<MovieModel>(10)
            .ToList();

        int page = _fixture.Create<int>();

        var movieSearchResult = new MovieSearchResult
        {
            Response = "True",
            Search = movies,
            TotalResults = movies.Count.ToString()
        };

        var res = new Result<MovieSearchResult>(movieSearchResult);

        _movieService.Setup(mvs => mvs.Search(searchTerm, page))
            .ReturnsAsync(new Result<MovieSearchResult>(movieSearchResult));

        var request = new SearchMovieRequest(searchTerm, page);

        var handler = new SearchMovieRequestHandler(_movieService.Object, _mapper);

        var result = await handler.Handle(request, default);

        using var _ = new AssertionScope();

        result.IsSuccess.Should().BeTrue();

        result.Value.Movies.Should().BeOfType<List<MovieResponse>>();

        result.Value.Should().BeOfType<SearchMovieResponse>();

        result.Value.TotalRecords.Should().Be(result.Value.Movies.Count);
    }

    [Fact]
    public async Task SearchMovie_WithInvalidSearhTerm_ReturnsValidationError()
    {
        string searchTerm = string.Empty;

        int page = _fixture.Create<int>();

        var request = new SearchMovieRequest(searchTerm, page);

        var handler = new SearchMovieRequestHandler(_movieService.Object, default);

        var result = await handler.Handle(request, default);

        result.IsFailure.Should().BeTrue();

        result.Error.GetType().Should().Be(typeof(ValidationError));

        result.Error.Message.Should().Be("Search term can not be null or empty");
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
}
