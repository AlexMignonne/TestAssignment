namespace Addresses.Api.App.Queries.GetByIdProvince
{
    public sealed class GetByIdProvinceDto
    {
        public GetByIdProvinceDto(
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
