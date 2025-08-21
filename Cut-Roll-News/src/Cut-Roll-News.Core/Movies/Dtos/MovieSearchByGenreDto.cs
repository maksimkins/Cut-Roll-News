namespace Cut_Roll_News.Core.Movies.Dtos;

public class MovieSearchByGenreDto
{
    public Guid? GenreId { get; set; }
    public string? Name { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
