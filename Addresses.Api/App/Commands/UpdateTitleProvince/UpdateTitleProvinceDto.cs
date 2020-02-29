namespace Addresses.Api.App.Commands.UpdateTitleProvince
{
    public sealed class UpdateTitleProvinceDto
    {
        public UpdateTitleProvinceDto(
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
