namespace Cut_Roll_News.Core.MovieOriginCountries.Dtos;

public class MovieOriginCountryDto
{
    public Guid MovieId { get; set; }

    public required string CountryCode { get; set; }
}
