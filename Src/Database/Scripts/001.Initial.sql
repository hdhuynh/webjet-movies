
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieSummaries](
	[MovieId] [nvarchar](20) NOT NULL,
	[Title] [nvarchar](256) NOT NULL,
	[Poster] [nvarchar](2048) NOT NULL,
    [Price] [money] NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[UpdatedAt] [datetimeoffset](7) NOT NULL,	
	CONSTRAINT [PK_MovieSummaries] PRIMARY KEY CLUSTERED ([MovieId]) 
)
GO
CREATE TABLE [dbo].[MovieDetails](
	[MovieId] [nvarchar](20) NOT NULL,
	[Year] [SMALLINT] NULL,
	[Rated] [nvarchar](5) NULL,
	[Released] [nvarchar](100) NULL,
    [Runtime] [nvarchar](100) NULL,
	[Genre] [nvarchar](100) NULL,
	[Director] [nvarchar](100) NULL,
	[Writer] [nvarchar](100) NULL,
	[Actors] [nvarchar](500) NULL,
	[Plot] [nvarchar](1000) NULL,
	[Language] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NULL,
	[Awards] [nvarchar](100) NULL,
	[Metascore] [SMALLINT] NULL,
	[Rating] DECIMAL(3, 1) NULL,
	[Votes] [nvarchar](100) NULL,
	[UpdatedAt] [datetimeoffset](7) NOT NULL,	
	CONSTRAINT [PK_MovieDetails] PRIMARY KEY CLUSTERED ([MovieId]) ,
	CONSTRAINT [FK_MovieDetails_MovieSummaries] FOREIGN KEY ([MovieId]) REFERENCES [dbo].[MovieSummaries] ([MovieId])

)
GO
