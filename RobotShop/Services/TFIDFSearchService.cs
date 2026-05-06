using System;
using System.Collections.Generic;
using System.Linq;
using RobotShop.Services.Interfaces;

public class TFIDFSearchService : ITFIDFSearchService
{
	private Dictionary<string, double> idfScores = new Dictionary<string, double>();

	
	private string NormalizeText(string text)
	{
		
		text = text.ToLower();
		char[] specialChars = [',', '.', '-', '(','"', ')', '\''];
		foreach (char c in specialChars)
		{
			text = text.Replace(c.ToString(), " ");
		}
		return text.Trim();
	}

	
	public void ComputeIDFScores(List<string> productTitles)
	{
		int totalDocuments = productTitles.Count;
		Dictionary<string, int> documentFrequency = new Dictionary<string, int>();

		foreach (var title in productTitles)
		{
			var words = NormalizeText(title).Split(' ').Distinct();
			foreach (var word in words)
			{
				if (!documentFrequency.ContainsKey(word))
					documentFrequency[word] = 1;
				else
					documentFrequency[word]++;
			}
		}

		foreach (var word in documentFrequency.Keys)
		{
			idfScores[word] = Math.Log((double)totalDocuments / (1 + documentFrequency[word]));
		}
	}
	

	public double ComputeTFIDF(string productTitle, string searchQuery)
	{
		if (idfScores == null || idfScores.Count == 0)
			throw new Exception("IDF Scores must be computed before running search.");

		var titleWords = NormalizeText(productTitle).Split(' ');
		var queryWords = NormalizeText(searchQuery).Split(' ');

		double score = 0.0;
		int matchedWords = 0;

		foreach (var word in queryWords)
		{
			if (titleWords.Any(titleWord => titleWord.Contains(word))) // 
			{
				matchedWords++;
			}
		}

		if (matchedWords == queryWords.Length) 
		{
			foreach (var word in queryWords)
			{
				foreach (var titleWord in titleWords)
				{
					if (titleWord.Contains(word))
					{
						int termFrequency = titleWords.Count(w => w.Contains(word));
						if (idfScores.ContainsKey(titleWord))
						{
							score += (termFrequency / (double)titleWords.Length) * idfScores[titleWord];
						}
					}
				}
			}
		}

		return score;
	}

	public List<string> GetTopSuggestions(string searchTerm, List<string> productTitles)
	{
		searchTerm = NormalizeText(searchTerm);
		var queryWords = searchTerm.Split(' ');

		var rankedTitles = productTitles
			.Select(title => new { Name = title, Score = ComputeTFIDF(title, searchTerm) })
			.Where(result => result.Score > 0 && queryWords.All(q => NormalizeText(result.Name).Contains(q)))
			.OrderByDescending(result => result.Score)
			.ThenBy(title => title.Name.Length) 
			.Select(result => result.Name)
			.Take(5)
			.ToList();

		return rankedTitles;
	}



}
