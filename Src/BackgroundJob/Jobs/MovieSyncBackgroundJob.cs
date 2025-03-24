using Serilog;
using System.Globalization;
using System.Linq;
using Webjet.Backend.Common.Configuration;
using Webjet.Backend.Movies.GetMovieList;
using Webjet.Backend.Repositories.Write;
using Webjet.Backend.Services;

namespace BackgroundJob.Jobs;

public class MovieSyncBackgroundJob(IMovieProviderApiService movieProviderApiService, IMovieWriteRepository repository)
{
    private readonly ILogger _log = Log.ForContext<MovieSyncBackgroundJob>();

    [FunctionName("SyncReminderJob")]
	public async Task Run([TimerTrigger("%Jobs:SyncReminder:Trigger%")] TimerInfo timer)
	{
		_log.VerboseEvent("Sync", "Starting sync job");

		try
        {
            await SyncMovies();
        }
        catch (Exception e)
		{
			_log.ErrorEvent("Sync", e, "Error processing sync reminder job");
		}

        _log.VerboseEvent("Sync", "Finished sync job");
	}

    private async Task SyncMovies()
    {
        var movieDtoList = await MergeLatestMovieList();
        // Parallel.ForEach(movieDtoList, async (movieDto) =>
        // {
        //     try
        //     {
        //         await AddOrUpdateSingleMovie(movieDto);
        //     }
        //     catch (Exception e)
        //     {
        //         _log.With("Movie", movieDto)
        //             .ErrorEvent("Sync", e, $"Error processing movie {movieDto}");
        //     }
        // });

        foreach (var movieDto in movieDtoList)
        {
            try
            {
                await AddOrUpdateSingleMovie(movieDto);
            }
            catch (Exception e)
            {
                _log.With("Movie", movieDto)
                    .ErrorEvent("Sync", e, $"Error processing movie {movieDto}");
            }
        }
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

        //TODO: consider logic to merge movie details from 2 providers
        await repository.AddOrUpdateMovieSummary(movieDto, cineWorldMovieDetail);
    }

    private async Task<List<MovieDto>> MergeLatestMovieList()
    {
        var list1 = await movieProviderApiService.GetAllMovies(MovieProvider.CinemaWorld);
        var list2 = await movieProviderApiService.GetAllMovies(MovieProvider.FilmWorld);
        foreach (var movieDto in list1.Movies)
        {
            movieDto.Id = TrimMovieId(movieDto.Id);
        }

        var list1Ids = list1.Movies.Select(m => m.Id);
        foreach (var movieDto in list2.Movies)
        {
            if (!list1Ids.Contains(TrimMovieId(movieDto.Id)))
            {
                movieDto.Id = TrimMovieId(movieDto.Id);
                list1.Movies.Add(movieDto);
            }
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