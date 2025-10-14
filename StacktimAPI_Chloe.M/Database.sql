CREATE DATABASE StacktimDb;
GO

USE StacktimDb;
GO

CREATE TABLE Players (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Pseudo NVARCHAR(50) NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Rank NVARCHAR(20) NOT NULL,
    TotalScore INT NOT NULL DEFAULT 0,
    RegistrationDate DATETIME NOT NULL DEFAULT GETDATE(),
    

    CONSTRAINT CHK_Player_TotalScore_NonNegative CHECK (TotalScore >= 0),

    CONSTRAINT CHK_Player_Rank_Valid CHECK (Rank IN ('Bronze', 'Silver', 'Gold', 'Platinum', 'Diamond', 'Master'))
);
GO

