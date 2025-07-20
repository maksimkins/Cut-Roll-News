namespace Cut_Roll_News.Core.NewsArticles.Dtos;

public class NewsArticlePaginationDto
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}