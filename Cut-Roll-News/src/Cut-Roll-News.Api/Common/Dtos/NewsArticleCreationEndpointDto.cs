namespace Cut_Roll_News.Api.Common.Dtos;

public class NewsArticleCreationEndpointDto
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required string AuthorId { get; set; }
    public required string ReferencesJson { get; set; }
    public IFormFile? Photo { get; set; }
}
