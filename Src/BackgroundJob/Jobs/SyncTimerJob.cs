using Serilog;
using System.Globalization;
using System.Linq;
using Webjet.Backend.Common.Configuration;
using Webjet.Backend.Movies.GetMovieList;
using Webjet.Backend.Repositories.Write;
using Webjet.Backend.Services;

namespace BackgroundJob.Jobs;

public class SyncTimerJob(IMovieProviderApiService movieProviderApiService, IMovieWriteRepository repository) //, MyDBContext context)
{
    private readonly ILogger _log = Log.ForContext<SyncTimerJob>();

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
        //var moviesListDto = await movieProviderApiService.GetAllMovies(movieProvider);
        var movieDtoList = await MergeLatestMovieList();

        foreach (var movieDto in movieDtoList)
        {
            try
            {
                movieDto.Price = null;
                var movieDetails1 = await movieProviderApiService.GetMovieDetails(MovieProvider.CinemaWorld, ToFullMovieId(movieDto.Id, MovieProvider.CinemaWorld));
                var movieDetails2 = await movieProviderApiService.GetMovieDetails(MovieProvider.FilmWorld, ToFullMovieId(movieDto.Id, MovieProvider.FilmWorld));
                var price1 = Convert.ToDecimal(movieDetails1.Price);
                var price2 = Convert.ToDecimal(movieDetails2.Price);
                movieDto.Price = Math.Min(price1, price2).ToString(CultureInfo.InvariantCulture);
                await repository.AddOrUpdateMovieSummary(movieDto);
            }
            catch (Exception e)
            {
                _log.With("Movie", movieDto)
                    .ErrorEvent("Sync", e, $"Error processing movie {movieDto}");
            }
        }
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