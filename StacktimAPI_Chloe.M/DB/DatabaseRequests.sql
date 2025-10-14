-- leaderboard
SELECT Pseudo, TotalScore FROM Players ORDER BY TotalScore DESC;

-- team composition
SELECT Teams.Name, Players.Pseudo, TeamPlayers.Role FROM Teams 
JOIN TeamPlayers ON Teams.Id = TeamPlayers.TeamId
JOIN Players ON Players.Id = TeamPlayers.PlayerId;

-- team stats 
SELECT Teams.Name, COUNT(TeamPlayers.PlayerId) AS "Players count", AVG(Players.TotalScore) AS "Average score" FROM Teams
JOIN TeamPlayers ON Teams.Id = TeamPlayers.TeamId
JOIN Players ON Players.Id = TeamPlayers.PlayerId
GROUP BY Teams.Name;

-- unused players
SELECT Players.Pseudo, Players.Email, Players.Rank, Players.TotalScore FROM Players
JOIN TeamPlayers ON Players.Id = TeamPlayers.PlayerId
WHERE TeamPlayers.PlayerId IS NULL;