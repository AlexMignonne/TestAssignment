namespace Addresses.SharedLibrary.ViewModels
{
    public sealed class AddressViewModel
    {
        public int CountryId { get; set; }
        public string? CountryTitle { get; set; }
        public int ProvinceId { get; set; }
        public string? ProvinceTitle { get; set; }
    }
}
