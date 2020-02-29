using System.Collections.Generic;

namespace Addresses.Api.App.Queries.GetByIdCountry
{
    public sealed class GetByIdCountryDto
    {
        public GetByIdCountryDto(
            int id,
            string title,
            IEnumerable<GetByIdCountryProvinceDto> provinces)
        {
            Id = id;
            Title = title;
            Provinces = provinces;
        }

        public int Id { get; }
        public string Title { get; }
        public IEnumerable<GetByIdCountryProvinceDto> Provinces { get; }
    }
}
