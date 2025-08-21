namespace Cut_Roll_News.Core.Movies.Dtos;

using Cut_Roll_News.Core.MovieImages.Models;

public class MovieSimplifiedDto
{
    public Guid MovieId { get; set; }
    public required string Title { get; set; }
    public MovieImage? Poster { get; set; }
}
