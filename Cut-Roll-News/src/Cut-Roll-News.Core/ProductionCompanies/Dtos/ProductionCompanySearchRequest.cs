namespace Cut_Roll_News.Core.ProductionCompanies.Dtos;

public class ProductionCompanySearchRequest
{
    public string? Name { get; set; }
    public string? CountryCode { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
