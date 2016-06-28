USE master
GO

IF EXISTS (SELECT NULL FROM sys.databases WHERE name = N'TDDDemoAppDb') 
	USE [TDDDemoAppDb]
	GO
    DROP TABLE [Messages];
GO

