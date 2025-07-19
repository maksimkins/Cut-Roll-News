using Cut_Roll_News.Core.NewsReferences.Enums;

namespace Cut_Roll_News.Core.NewsReferences.Dtos;


public class NewsReferenceCreateDto
{
    public ReferenceType ReferenceType { get; set; }
    public required string ReferenceId { get; set; }
    public string? ReferenceUrl { get; set; }
}