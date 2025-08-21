namespace Cut_Roll_News.Core.People.Dtos;

public class PersonSearchRequest
{
    public required string Name { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
