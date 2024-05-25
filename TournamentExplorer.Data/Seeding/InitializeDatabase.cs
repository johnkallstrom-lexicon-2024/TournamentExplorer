using Bogus;
using TournamentExplorer.Core.Entities;
using TournamentExplorer.Core.Enums;
using TournamentExplorer.Data.Seeding.Fakers;

namespace TournamentExplorer.Data.Seeding
{
    public static class InitializeDatabase
    {
        private static TournamentFaker _tournamentFaker = new();
        private static Faker _faker = new Faker();

        public static async Task SeedAsync(TournamentExplorerDbContext context)
        {
            var tournaments = _tournamentFaker.Generate(50);

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

                for (int i = 1; i <= _faker.Random.Int(min: 5, max: 15); i++)
                {
                    var year = tournament.StartDate.Year;
                    var month = tournament.StartDate.Month;
                    var day = tournament.StartDate.AddDays(_faker.Random.Int(min: 1, max: 10)).Day;

                    games.Add(new Game
                    {
                        Name = $"Game {i}",
                        Time = _faker.Date.Between(tournament.StartDate, tournament.StartDate.AddMonths(3)),
                        Duration = duration,
                        TournamentId = tournament.Id,
                        Tournament = tournament
                    });
                }
            }

            return games;
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
