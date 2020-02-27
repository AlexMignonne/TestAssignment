using Addresses.Api.DataTransferObjects;
using CommonLibrary.Messages;
using MediatR;

namespace Addresses.Api.App.Queries
{
    public sealed class GetByProvinceIdAddressQuery
        : Command,
            IRequest<ProvinceDto>
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
