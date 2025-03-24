using Serilog;
using Webjet.Backend.Common.Configuration;
using Webjet.Backend.Handlers;

namespace BackgroundJob.Jobs;

public class MovieSyncBackgroundJob(IMovieSyncHandler movieSyncHandler)
{
    private readonly ILogger _log = Log.ForContext<MovieSyncBackgroundJob>();
    private bool _syncInProgress = false;

    [FunctionName("SyncReminderJob")]
    public async Task Run([TimerTrigger("%Jobs:SyncReminder:Trigger%")] TimerInfo timer)
    {
        _log.VerboseEvent(Constants.Logging.API_SYNC, "Starting sync job");
        if (_syncInProgress)
        {
            _log.VerboseEvent(Constants.Logging.API_SYNC, "Sync job already in progress, skipping");
            return;
        }

        try
        {
            _syncInProgress = true;
            await movieSyncHandler.SyncMovies();
        }
        catch (Exception e)
        {
            _log.ErrorEvent(Constants.Logging.API_SYNC, e, "Error processing sync job");
        }
        finally
        {
            _syncInProgress = false;
        }

        _log.VerboseEvent(Constants.Logging.API_SYNC, "Finished sync job");
    }
}