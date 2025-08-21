namespace Cut_Roll_News.Core.Casts.Dtos;

public class CastGetByPersonIdDto
{
    public Guid PersonId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
