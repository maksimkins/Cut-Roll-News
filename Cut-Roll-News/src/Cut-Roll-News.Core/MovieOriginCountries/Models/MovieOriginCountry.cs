#pragma warning disable CS8618

namespace Cut_Roll_News.Core.MovieOriginCountries.Models;

using System.Text.Json.Serialization;
using Cut_Roll_News.Core.Countries.Models;
using Cut_Roll_News.Core.Movies.Models;

public class MovieOriginCountry
{
    public Guid MovieId { get; set; }
    public required string CountryCode { get; set; }
    [JsonIgnore]
    public Movie Movie { get; set; }
    public Country Country { get; set; }
}