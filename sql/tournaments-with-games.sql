SELECT 
T.Id,
T.Title,
T.StartDate,
G.Name,
G.Time,
G.Duration
FROM Game AS G
INNER JOIN Tournament AS T ON G.TournamentId = T.Id