#pragma warning disable CS8618 

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cut_Roll_News.Core.Common.Models.Base;
using Cut_Roll_News.Core.NewsArticles.Models;

namespace Cut_Roll_News.Core.Users.Models;

public class User : IBanable, IMuteable
{
    [Key]
    public required string Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public string? AvatarPath { get; set; }

    [DefaultValue(false)]
    public bool IsBanned { get; set; }

    [DefaultValue(false)]
    public bool IsMuted { get; set; }
    public List<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
}