using Serilog;
using Webjet.Backend.Common.Configuration;
using Webjet.Backend.Repositories.Write;
using Webjet.Backend.Services;
using Webjet.Backend.Services.Movies.GetMovieList;

namespace Webjet.Backend.Handlers
{
    public class MovieSyncHandler(IMovieProviderApiService movieProviderApiService, IMovieWriteRepository repository): IMovieSyncHandler
    {
        private readonly ILogger _log = Log.ForContext<MovieSyncHandler>();

        public async Task SyncMovies()
        {
            var movieDtoList = await MergeLatestMovieList();

            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = 3
            };

            await Parallel.ForEachAsync(movieDtoList, parallelOptions, async (movieDto, token) =>
            {
                try
                {
                    await AddOrUpdateSingleMovie(movieDto);
                }
                catch (Exception e)
                {
                    _log.Error(Constants.Logging.API_SYNC, e, $"Error processing movie {movieDto}");
                }
            });
        }

        private async Task AddOrUpdateSingleMovie(MovieDto movieDto)
        {
            var cineWorldMovieDetail = await movieProviderApiService.GetMovieDetails(MovieProvider.CinemaWorld, ToFullMovieId(movieDto.Id, MovieProvider.CinemaWorld));
            var filmWorldMovieDetail = await movieProviderApiService.GetMovieDetails(MovieProvider.FilmWorld, ToFullMovieId(movieDto.Id, MovieProvider.FilmWorld));
            if (Convert.ToDecimal(cineWorldMovieDetail.Price) <= Convert.ToDecimal(filmWorldMovieDetail.Price))
            {
                movieDto.Price = cineWorldMovieDetail.Price;
                movieDto.BestPriceProvider = MovieProvider.CinemaWorld.ToString();
            }
            else
            {
                movieDto.Price = filmWorldMovieDetail.Price;
                movieDto.BestPriceProvider = MovieProvider.FilmWorld.ToString();
            }

            //TODO: consider logic to merge movie details from 2 providers in case there are differences
            await repository.AddOrUpdateMovieSummary(movieDto, cineWorldMovieDetail);
        }

        private async Task<List<MovieDto>> MergeLatestMovieList()
        {
            var list1 = await movieProviderApiService.GetAllMovies(MovieProvider.CinemaWorld);
            var list2 = await movieProviderApiService.GetAllMovies(MovieProvider.FilmWorld);
            foreach (var movieDto in list1.Movies)
                movieDto.Id = TrimMovieId(movieDto.Id);

            var list1Ids = list1.Movies.Select(m => m.Id);
            foreach (var movieDto in list2.Movies)
                if (!list1Ids.Contains(TrimMovieId(movieDto.Id)))
                {
                    movieDto.Id = TrimMovieId(movieDto.Id);
                    list1.Movies.Add(movieDto);
                }
            return list1.Movies;
        }

        private string TrimMovieId(string originalId)
        {
            return originalId.Substring(2);
        }

        private string ToFullMovieId(string trimMovieId, MovieProvider movieProvider)
        {
            return movieProvider == MovieProvider.CinemaWorld ? $"cw{trimMovieId}" : $"fw{trimMovieId}";
        }
    }
}
