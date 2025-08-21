namespace Cut_Roll_News.Core.Crews.Dtos;

public class CrewSearchDto
{
    public string? Name { get; set; }
    public string? JobTitle { get; set; }
    public string? Department { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
