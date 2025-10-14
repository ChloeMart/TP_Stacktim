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

INSERT INTO Players (Pseudo, Email, Rank, TotalScore) VALUES
('Livio', 'livio@stacktim.com', 'Master', 2500),
('Dan', 'dan@stacktim.com', 'Gold', 850),
('Florent', 'florent@stacktim.com', 'Silver', 400),
('Enys', 'enys@stacktim.com', 'Platinum', 1500),
('Victor', 'victor@stacktim.com', 'Diamond', 1800),
('Diana', 'diana@stacktim.com', 'Bronze', 50);
GO

CREATE TABLE Teams (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE,
    Tag CHAR(3) NOT NULL UNIQUE,
    CaptainId INT NOT NULL,
    CreationDate DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Team_CaptainId FOREIGN KEY (CaptainId) REFERENCES Players (Id),

    CONSTRAINT CHK_Team_Tag_Format CHECK (Tag LIKE '[A-Z][A-Z][A-Z]')
);
GO

INSERT INTO Teams (Name, Tag, CaptainId) VALUES
('Stacktim Dailybiz', 'STD', 1),
('Stacktim Idcom', 'STI', 3),
('Stacktim Software', 'STS', 5);
GO

CREATE TABLE TeamPlayers (
    TeamId INT NOT NULL,
    PlayerId INT NOT NULL,
    Role INT NOT NULL,
    JoinDate DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_TeamPlayers PRIMARY KEY (TeamId, PlayerId),

    CONSTRAINT FK_TeamPlayers_TeamId FOREIGN KEY (TeamId) REFERENCES Teams (Id) ON DELETE CASCADE,

    CONSTRAINT FK_TeamPlayers_PlayerId FOREIGN KEY (PlayerId) REFERENCES Players (Id) ON DELETE CASCADE,

    CONSTRAINT CHK_TeamPlayers_Role_Valid CHECK (Role IN (0, 1, 2))
);
GO

INSERT INTO TeamPlayers (TeamId, PlayerId, Role) VALUES
(1, 1, 0),
(1, 2, 1),
(1, 6, 2),
(2, 3, 0),
(2, 4, 1),
(2, 1, 2),
(3, 5, 0),
(3, 6, 1),
(3, 3, 2);
GO