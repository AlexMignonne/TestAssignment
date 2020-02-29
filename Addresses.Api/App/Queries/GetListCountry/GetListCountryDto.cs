namespace Addresses.Api.App.Queries.GetListCountry
{
    public sealed class GetListCountryDto
    {
        public GetListCountryDto(
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
