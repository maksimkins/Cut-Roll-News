namespace Cut_Roll_News.Core.ProductionCompanies.Dtos;

public class ProductionCompanyUpdateDto
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }

    public string? CountryCode { get; set; }

    public string? LogoPath { get; set; }
}
