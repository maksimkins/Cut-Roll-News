using System.Text.Json.Serialization;
using Cut_Roll_News.Core.MovieKeywords.Models;

namespace Cut_Roll_News.Core.Keywords.Models;

public class Keyword
{
    public Guid Id { get; set; }

    public required string Name { get; set; }
    [JsonIgnore]
    
    public ICollection<MovieKeyword> MovieKeywords { get; set; } = [];
}
