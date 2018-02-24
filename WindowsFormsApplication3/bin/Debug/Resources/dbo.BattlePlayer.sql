CREATE TABLE [dbo].[BattlePlayer] (
    [Id]           INT        IDENTITY (1, 1) NOT NULL,
    [Name]         NCHAR (30) NOT NULL,
    [WinAmount]    INT        NULL,
    [DefeatAmount] INT        NULL,
    [Rating]       FLOAT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

