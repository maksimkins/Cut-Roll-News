using Cut_Roll_News.Core.NewsReferences.Enums;

namespace Cut_Roll_News.Core.NewsReferences.Dtos;
public class NewsReferenceDeleteDto
{
    public ReferenceType ReferenceType { get; set; }
    public required Guid? ReferencedId { get; set; }
}
