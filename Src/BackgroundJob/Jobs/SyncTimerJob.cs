using Serilog;
using Webjet.Backend.Common.Configuration;
using Webjet.Backend.Services;

namespace BackgroundJob.Jobs;

public class SyncTimerJob(IMovieProviderApiService movieProviderApiService)
{
    private readonly ILogger _log = Log.ForContext<SyncTimerJob>();

    [FunctionName("SyncReminderJob")]
	public async Task Run([TimerTrigger("%Jobs:SyncReminder:Trigger%")] TimerInfo timer)
	{
		_log.VerboseEvent("Sync", "Starting sync job");

		try
        {
            var movies = await movieProviderApiService.GetAllMovies(MovieProvider.CinemaWorld);
        }
		catch (Exception e)
		{
			_log.ErrorEvent("Sync", e, "Error processing sync reminder job");
		}

        _log.VerboseEvent("Sync", "Finished sync job");
	}
}