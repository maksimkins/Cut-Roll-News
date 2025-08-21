using System.Text.Json.Serialization;
using Cut_Roll_News.Core.Countries.Models;
using Cut_Roll_News.Core.MovieProductionCompanies.Models;

namespace Cut_Roll_News.Core.ProductionCompanies.Models;

public class ProductionCompany
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public string? CountryCode { get; set; }

    public string? LogoPath { get; set; }

    public Country? Country { get; set; }
    [JsonIgnore]

    public ICollection<MovieProductionCompany> MovieProductionCompanies { get; set; } = [];
}
