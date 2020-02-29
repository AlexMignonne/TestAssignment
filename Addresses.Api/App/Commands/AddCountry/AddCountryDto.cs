namespace Addresses.Api.App.Commands.AddCountry
{
    public sealed class AddCountryDto
    {
        public AddCountryDto(
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
