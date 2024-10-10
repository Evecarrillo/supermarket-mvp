CREATE TABLE [dbo].[PayMode] (
    [Pay_Mode_Id]          INT           IDENTITY (100000, 1) NOT NULL,
    [Pay_Mode_Name]        NVARCHAR (50) NOT NULL,
    [Pay_Mode_Observation] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Pay_Mode_Id] ASC)
);

