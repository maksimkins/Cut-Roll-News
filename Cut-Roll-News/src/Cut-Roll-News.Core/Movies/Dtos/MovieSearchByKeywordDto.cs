namespace Cut_Roll_News.Core.Movies.Dtos;

public class MovieSearchByKeywordDto
{
    public string? Name { get; set; }
    public Guid? KeywordId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
