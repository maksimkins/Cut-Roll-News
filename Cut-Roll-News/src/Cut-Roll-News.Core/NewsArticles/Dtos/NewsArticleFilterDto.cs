namespace Cut_Roll_News.Core.NewsArticles.Dtos;

using Cut_Roll_News.Core.NewsReferences.Dtos;

public class NewsArticleFilterDto
{
    public string? Query { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public List<NewsReferenceSearchByDto>? ReferenceToSearch { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}