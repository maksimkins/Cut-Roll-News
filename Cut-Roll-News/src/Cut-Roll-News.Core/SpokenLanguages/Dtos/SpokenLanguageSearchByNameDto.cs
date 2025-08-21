namespace Cut_Roll_News.Core.SpokenLanguages.Dtos;

public class SpokenLanguageSearchByNameDto
{
    public required string Name { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10; 
}