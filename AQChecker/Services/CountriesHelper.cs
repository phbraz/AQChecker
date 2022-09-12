using System.Globalization;
using AQChecker.DTO;
using ISO3166;

namespace AQChecker.Services;

public class CountriesHelper
{
    public static List<CountryDto> GetCountriesList()
    {
        var countriesList = ISO3166.Country.List;

        var resultList = countriesList.Select(x => new CountryDto
        {
            Name = x.Name,
            CountryCode = x.TwoLetterCode
        }).ToList();

        return resultList;

    }
}