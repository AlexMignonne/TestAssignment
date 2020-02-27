using System.Collections.Generic;

namespace Addresses.Api.DataTransferObjects
{
    public sealed class CountryDto
    {
        public CountryDto(
            int id,
            string title,
            List<ProvinceDto> provinces)
        {
            Id = id;
            Title = title;
            Provinces = provinces;
        }

        public int Id { get; }
        public string Title { get; }
        public List<ProvinceDto> Provinces { get; }
    }
}
