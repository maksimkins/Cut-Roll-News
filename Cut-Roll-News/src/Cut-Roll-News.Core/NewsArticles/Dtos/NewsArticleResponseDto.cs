using System.ComponentModel;
using Cut_Roll_News.Core.NewsReferences.Models;
using Cut_Roll_News.Core.Users.Dtos;

namespace Cut_Roll_News.Core.NewsArticles.Dtos;

public class NewsArticleResponseDto
{
    public Guid Id { get; set; } 
    public required string AuthorId { get; set; }
    public required UserSimplified Author { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public string? PhotoPath { get; set; }
    [DefaultValue(0)]
    public int LikesCount { get; set; }
    public ICollection<NewsReference>? NewsReferences { get; set; }
}
