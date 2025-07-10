namespace Cut_Roll_News.Core.NewsArticles.Dtos;

public class NewsArticleUpdateContentDto
{
    public required string Id { get; set; }
    public string? NewTitle { get; set; }
    public string? NewContent { get; set; }
}
