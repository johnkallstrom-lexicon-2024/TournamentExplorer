using Bogus;
using TournamentExplorer.Core.Entities;
using TournamentExplorer.Core.Enums;

namespace TournamentExplorer.Data
{
    public static class InitializeDatabase
    {
        private static Faker _faker = new Faker();

        public static async Task SeedAsync(TournamentExplorerDbContext context)
        {
            var tournaments = GenerateTournaments();
            var games = GenerateGames(tournaments);

            if (tournaments != null && tournaments.Count() > 0)
            {
                await context.Tournaments.AddRangeAsync(tournaments);
            }

            if (games != null && games.Count() > 0)
            {
                await context.Games.AddRangeAsync(games);
            }

            await context.SaveChangesAsync();
        }

        public static IEnumerable<Game> GenerateGames(IEnumerable<Tournament> tournaments)
        {
            var games = new List<Game>();

            foreach (var tournament in tournaments)
            {
                int duration = GetGameDuration(tournament.Type);

                for (int i = 1; i <= _faker.Random.Int(min: 1, max: 7); i++)
                {
                    var year = tournament.StartDate.Year;
                    var month = tournament.StartDate.Month;
                    var day = tournament.StartDate.AddDays(_faker.Random.Int(min: 1, max: 10)).Day;

                    games.Add(new Game
                    {
                        Name = $"Game {i}",
                        Time = new DateTime(year, month, day),
                        Duration = duration,
                        TournamentId = tournament.Id,
                        Tournament = tournament
                    });
                }
            }

            return games;
        }

        public static IEnumerable<Tournament> GenerateTournaments()
        {
            var tournaments = new List<Tournament>();

            var year = DateTime.Now.Year;
            var nextMonth = DateTime.Now.AddMonths(1).Month;
            var startDate = new DateTime(year, nextMonth, day: 1, hour: 10, minute: 00, second: 00);

            var tournamentTypes = Enum.GetValues<TournamentType>();

            for (int i = 0; i < tournamentTypes.Length; i++)
            {
                tournaments.Add(new Tournament
                {
                    Title = $"{tournamentTypes[i]} Tournament",
                    StartDate = startDate,
                    City = _faker.Address.City(),
                    Country = _faker.Address.Country(),
                    Type = tournamentTypes[i],
                });
            }

            return tournaments;
        }

        public static int GetGameDuration(TournamentType type)
        {
            switch (type)
            {
                case TournamentType.Football:
                    return 90;
                case TournamentType.Icehockey:
                    return 60;
                case TournamentType.Poker:
                    return 120;
                case TournamentType.Volleyball:
                    return 90;
                case TournamentType.Golf:
                    return 240;
                case TournamentType.Tennis:
                    return 120;
                default:
                    return 60;
            }
        }
    }
}
