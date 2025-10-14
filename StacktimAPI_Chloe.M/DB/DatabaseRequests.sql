-- leaderboard
SELECT Pseudo, TotalScore FROM Players ORDER BY TotalScore DESC;

-- team composition
SELECT Teams.Name, Players.Pseudo, TeamPlayers.Role FROM Teams 
JOIN TeamPlayers ON Teams.Id = TeamPlayers.TeamId
JOIN Players ON Players.Id = TeamPlayers.PlayerId;