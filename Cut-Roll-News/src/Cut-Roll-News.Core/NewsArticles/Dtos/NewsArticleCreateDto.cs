
using Cut_Roll_News.Core.NewsReferences.Dtos;
using Microsoft.AspNetCore.Http;

namespace Cut_Roll_News.Core.NewsArticles.Dtos;

public class NewsArticleCreateDto
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required string AuthorId { get; set; }
    public List<NewsReferenceCreateDto> References { get; set; } = new();
    public IFormFile? Photo { get; set; }
}
