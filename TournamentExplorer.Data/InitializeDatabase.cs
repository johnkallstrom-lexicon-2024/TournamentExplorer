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
                for (int i = 1; i <= _faker.Random.Int(min: 1, max: 5); i++)
                {
                    games.Add(new Game
                    {
                        Name = $"{tournament.Type} Game",
                        Time = tournament.StartDate.AddDays(_faker.Random.Int(min: 1, max: 7)),
                        Duration = _faker.Random.Int(min: 30, max: 120),
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
            var tournamentTypesLength = Enum.GetValues<TournamentType>().Length;

            for (int i = 1; i <= 5; i++)
            {
                tournaments.Add(new Tournament
                {
                    Title = $"Tournament {i}",
                    Description = _faker.Commerce.ProductDescription(),
                    Location = _faker.Address.FullAddress(),
                    StartDate = DateTime.Now,
                    Type = (TournamentType)new Random().Next(0, (tournamentTypesLength - 1)),
                });
            }

            return tournaments;
        }
    }
}
