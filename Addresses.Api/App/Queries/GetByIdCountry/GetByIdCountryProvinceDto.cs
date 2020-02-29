namespace Addresses.Api.App.Queries.GetByIdCountry
{
    public sealed class GetByIdCountryProvinceDto
    {
        public GetByIdCountryProvinceDto(
            int id,
            string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; }
        public string Title { get; }
    }
}
