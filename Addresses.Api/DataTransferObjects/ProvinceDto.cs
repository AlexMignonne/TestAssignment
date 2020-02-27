namespace Addresses.Api.DataTransferObjects
{
    public sealed class ProvinceDto
    {
        public ProvinceDto(
            int id,
            int countryId,
            string title,
            CountryDto country)
        {
            Id = id;
            CountryId = countryId;
            Title = title;
            Country = country;
        }

        public int Id { get; }
        public int CountryId { get; }
        public string Title { get; }
        public CountryDto Country { get; }
    }
}
