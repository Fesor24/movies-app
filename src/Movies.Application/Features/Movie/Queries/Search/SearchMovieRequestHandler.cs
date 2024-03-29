﻿using AutoMapper;
using MediatR;
using Movies.Domain.Primitives;
using Movies.Domain.Services;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("Movies.UnitTests")]

namespace Movies.Application.Features.Movie.Queries.Search;
internal sealed class SearchMovieRequestHandler : 
    IRequestHandler<SearchMovieRequest, Result<SearchMovieResponse, Error>>
{
    private readonly IMovieService _movieService;
    private readonly IMapper _mapper;

    public SearchMovieRequestHandler(IMovieService movieService, IMapper mapper)
    {
        _movieService = movieService;
        _mapper = mapper;
    }

    public async Task<Result<SearchMovieResponse, Error>> Handle(SearchMovieRequest request, 
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.SearchTerm))
            return new Error("404", "Empty or null string passed as search term");

        var res = await _movieService.Search(request.SearchTerm, request.Page);

        if (res.IsFailure)
            return res.Error;

        var movies = res.Value.Search;

        var _ = int.TryParse(res.Value.TotalResults, out int totalRecords);

        return new SearchMovieResponse
        {
            Movies = _mapper.Map<List<MovieResponse>>(movies),
            TotalRecords = totalRecords
        };
    }
}
