using CommonLibrary.Messages;
using MediatR;

namespace Addresses.Api.App.Queries.GetByProvinceIdAddress
{
    public sealed class GetByProvinceIdAddressQuery
        : Command,
            IRequest<GetByProvinceIdAddressDto?>
    {
        public GetByProvinceIdAddressQuery(
            string correlationToken,
            int provinceId)
            : base(correlationToken)
        {
            ProvinceId = provinceId;
        }

        public int ProvinceId { get; }
    }
}
