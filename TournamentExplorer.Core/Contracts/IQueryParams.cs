namespace TournamentExplorer.Core.Contracts
{
    public interface IQueryParams
    {
        public string? SearchTerm { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
    }
}
