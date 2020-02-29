namespace Addresses.Api.App.Queries.GetByProvinceIdAddress
{
    public sealed class GetByProvinceIdAddressDto
    {
        public GetByProvinceIdAddressDto(
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
