CREATE TABLE [dbo].[tblUser] (
    [id]              INT            IDENTITY (1, 1) NOT NULL,
    [Name]            VARCHAR (100)  NOT NULL,
    [Surname]         VARCHAR (100)  NOT NULL,
    [Email]           VARCHAR (100)  NOT NULL,
    [Password]        VARCHAR (100)  NOT NULL,
    [Country]         VARCHAR (100)  NOT NULL,
    [FavouriteColour] VARCHAR (50)   NOT NULL,
    [Birthday]        DATE           NOT NULL,
    [CellphoneNumber] VARCHAR(50)            NOT NULL,
    [Comments]        VARCHAR (1000) NOT NULL,
    CONSTRAINT [PK_tblUser] PRIMARY KEY CLUSTERED ([id] ASC)
);

