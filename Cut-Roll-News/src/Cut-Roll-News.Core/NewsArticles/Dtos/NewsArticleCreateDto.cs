using Cut_Roll_News.Core.NewsReferences.Dtos;

namespace Cut_Roll_News.Core.NewsArticles.Dtos;

public class NewsArticleCreateDto
{
    public required string Title { get; set; }
    public required string Content { get; set; }   
    public List<NewsReferenceDto> References { get; set; } = new();
}
