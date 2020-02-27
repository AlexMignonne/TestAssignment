namespace Accounts.Api.DataTransferObjects
{
    public sealed class AddressDto
    {
        public AddressDto(
            int countryId,
            string countryTitle,
            int provinceId,
            string provinceTitle)
        {
            CountryId = countryId;
            CountryTitle = countryTitle;
            ProvinceId = provinceId;
            ProvinceTitle = provinceTitle;
        }

        public int CountryId { get; }
        public string CountryTitle { get; }
        public int ProvinceId { get; }
        public string ProvinceTitle { get; }
    }
}
