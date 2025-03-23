
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
	CONSTRAINT [PK_Movies] PRIMARY KEY CLUSTERED ([MovieId]) 
)
GO
