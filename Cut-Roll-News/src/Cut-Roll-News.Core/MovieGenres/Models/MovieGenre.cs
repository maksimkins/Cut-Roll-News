#pragma warning disable CS8618

using System.Text.Json.Serialization;
using Cut_Roll_News.Core.Genres.Models;
using Cut_Roll_News.Core.Movies.Models;

namespace Cut_Roll_News.Core.MovieGenres.Models;

public class MovieGenre
{
    public Guid MovieId { get; set; }
    public Guid GenreId { get; set; }
    [JsonIgnore]
    public Movie Movie { get; set; }
    public Genre Genre { get; set; }
}
