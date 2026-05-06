namespace RobotShop.Services.Interfaces
{
	public interface ITFIDFSearchService
	{
		void ComputeIDFScores(List<string> productTitles);
		double ComputeTFIDF(string productTitle, string searchQuery);
		List<string> GetTopSuggestions(string searchTerm, List<string> productTitles);
	}
}
