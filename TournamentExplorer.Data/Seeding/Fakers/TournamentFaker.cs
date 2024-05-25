using Bogus;
using TournamentExplorer.Core.Entities;
using TournamentExplorer.Core.Enums;

namespace TournamentExplorer.Data.Seeding.Fakers
{
    public class TournamentFaker : Faker<Tournament>
    {
        private DateTime startDate = new DateTime(
            year: DateTime.Now.Year, 
            month: DateTime.Now.AddMonths(1).Month, 
            day: 1);

        private TournamentType[] types = Enum.GetValues<TournamentType>();

        public TournamentFaker()
        {
            RuleFor(t => t.Title, f => f.Company.CompanyName());
            RuleFor(t => t.StartDate, startDate);
            RuleFor(t => t.City, f => f.Address.City());
            RuleFor(t => t.Country, f => f.Address.Country());
            RuleForType(typeof(TournamentType), f => (TournamentType)f.Random.Int(min: 0, max: types.Length - 1));
        }
    }
}
