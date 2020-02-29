namespace Addresses.Api.App.Commands.UpdateTitleCountry
{
    public sealed class UpdateTitleCountryDto
    {
        public UpdateTitleCountryDto(
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
