namespace Cut_Roll_News.Core.Crews.Dtos;

using Cut_Roll_News.Core.MovieImages.Models;
using Cut_Roll_News.Core.People.Models;

public class CrewGetDto
{
    public required Person Person { get; set; }
    public string? Job { get; set; }
    public string? Department { get; set; }
    public Guid MovieId { get; set; }
    public required string Title { get; set; }
    public MovieImage? Poster { get; set; }
}

