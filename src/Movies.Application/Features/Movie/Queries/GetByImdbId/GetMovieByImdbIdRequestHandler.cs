using AutoMapper;
using MediatR;
using Movies.Application.Services;
using Movies.Domain.Primitives;

namespace Movies.Application.Features.Movie.Queries.GetByImdbId;
internal sealed class GetMovieByImdbIdRequestHandler : IRequestHandler<GetMovieByImdbIdRequest, 
    Result<GetMovieResponse>>
{
    private readonly IMovieService _movieService;
    private readonly IMapper _mapper;

    public GetMovieByImdbIdRequestHandler(IMovieService movieService, IMapper mapper)
    {
        _movieService = movieService;
        _mapper = mapper;
    }

    public async Task<Result<GetMovieResponse>> Handle(GetMovieByImdbIdRequest request, 
        CancellationToken cancellationToken)
    {
        var res = await _movieService.GetMovieByImdbId(request.ImdbId);

        if (res.IsFailure)
            return res.Error;

        return _mapper.Map<GetMovieResponse>(res.Value);
    }
}
