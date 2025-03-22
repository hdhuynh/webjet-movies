using Serilog;

namespace BackgroundJob.Jobs;

public class SyncTimerJob
{
	private readonly ILogger _log = Log.ForContext<SyncTimerJob>();

	public SyncTimerJob()
	{
	}

	[FunctionName("SyncReminderJob")]
	public async Task Run([TimerTrigger("%Jobs:SyncReminder:Trigger%")] TimerInfo timer)
	{
		_log.VerboseEvent("Sync", "Starting sync job");

		try
		{
			
		}
		catch (Exception e)
		{
			_log.ErrorEvent("Sync", e, "Error processing sync reminder job");
		}

        _log.VerboseEvent("Sync", "Finished sync job");
	}
}