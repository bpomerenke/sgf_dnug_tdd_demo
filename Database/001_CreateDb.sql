USE master
GO

IF NOT EXISTS (SELECT NULL FROM sys.databases WHERE name = N'TDDDemoAppDb') 
    CREATE DATABASE [TDDDemoAppDb];
GO

