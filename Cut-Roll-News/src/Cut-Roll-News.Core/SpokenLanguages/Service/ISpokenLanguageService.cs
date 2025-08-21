namespace Cut_Roll_News.Core.SpokenLanguages.Service;

using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.SpokenLanguages.Dtos;
using Cut_Roll_News.Core.SpokenLanguages.Models;

public interface ISpokenLanguageService
{
    Task<PagedResult<SpokenLanguage>> GetAllSpokenLanguageAsync(SpokenLanguagePaginationDto? dto);
    Task<PagedResult<SpokenLanguage>> SearchSpokenLanguageByNameAsync(SpokenLanguageSearchByNameDto? dto);
    Task<SpokenLanguage?> GetSpokenLanguageByIsoCodeAsync(string? isoCode);
}
