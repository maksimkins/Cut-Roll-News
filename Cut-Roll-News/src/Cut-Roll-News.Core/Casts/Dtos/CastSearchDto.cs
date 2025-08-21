namespace Cut_Roll_News.Core.Casts.Dtos;

public class CastSearchDto
{
    public string? Name { get; set; }
    public string? CharacterName { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
