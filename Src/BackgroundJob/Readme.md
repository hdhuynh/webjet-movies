# Quickstart

## Run WebJob project locally

1. Start and initialize Azure Storage Emulator (https://learn.microsoft.com/en-us/azure/storage/common/storage-use-emulator#:~:text=in%20this%20article.-,Start%20and%20initialize%20the%20Storage%20Emulator,-To%20start%20the)
2. Set BackgroundJob as startup project
3. Run BackgroundJob as a console application

## Process sleep data by running the WebJob locally

1. Upload sleep data to Azure Blob Storage. One way to do it is to use the Firefly.Utility.TriggerSleepAlgorithmRun utility (see step 2 and 3).
2. Ensure App.config of Firefly.Utility.TriggerSleepAlgorithmRun has the correct settings for the Azure Storage account.
```
		<add key="AzureBlobStorage.AccountName" value="fireflyteststore" />
		<add key="AzureBlobStorage.AccessKey" value="uuhBwK+Oz1MNicC7bO+hZJztNGg8kSH7bZFvzzPomuhdKgmytmG7PEEVIJc+fG4C/aCq/NOIExAWtqGL3LGo/g==" /> 
```
3. Run Firefly.Utility.TriggerSleepAlgorithmRun utility to upload data to a cloud blob storage for a patient	
```
	TriggerSleepAlgorithmRun.exe 9274e2fa-d326-44db-a2b5-a583e10b8eb7 SleepData\ASleepSession
```
	

	
*Note: When running locally, the default behavior of Firefly.Web and BackgroundJob is to use InMemoryBus. 
This means that events published by the TriggerSleepAlgorithmRun utility  will not be consumed by the WebJobs because they are in 2 different processes. Communication between 2 processes can still be done by configuring both the utility and BackgroundJob to use the same AzureSerivceBus.

3. To trigger data processing run by consuming event in *InMemory* bus, add code to a WebJobs's function (e.g. SyncReminderJob) to publish DataFilesUploadCompleteEvent for a specific patient
```
		await Bus.Publish(new DataFilesUploadCompleteEvent
		{
			PatientId = Guid.Parse("9274e2fa-d326-44db-a2b5-a583e10b8eb7"),
			CorrelationId = Guid.NewGuid()
		});														
```
4. Run WebJob project as a console application. The WebJob consumes the event published by the function, and processes the data uploaded by the utility

