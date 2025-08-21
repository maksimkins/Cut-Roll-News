using System.Text.Json.Serialization;
using Cut_Roll_News.Core.Casts.Models;
using Cut_Roll_News.Core.Crews.Models;

namespace Cut_Roll_News.Core.People.Models;

public class Person
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public string? ProfilePath { get; set; }
    [JsonIgnore]

    public ICollection<Cast> CastRoles { get; set; } = [];
    [JsonIgnore]

    public ICollection<Crew> CrewRoles { get; set; } = [];
}
