using Cut_Roll_News.Core.NewsReferences.Enums;

namespace Cut_Roll_News.Core.NewsReferences.Dtos;

public class NewsReferenceSearchByDto
{
    public ReferenceType ReferenceType { get; set; }
    public required string ReferenceId { get; set; }
}