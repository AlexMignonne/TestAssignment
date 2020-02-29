namespace Addresses.Api.App.Commands.AddProvince
{
    public sealed class AddProvinceDto
    {
        public AddProvinceDto(
            int id,
            int countryId,
            string title)
        {
            Id = id;
            CountryId = countryId;
            Title = title;
        }

        public int Id { get; }
        public int CountryId { get; }
        public string Title { get; }
    }
}
