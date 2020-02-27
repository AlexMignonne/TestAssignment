using Addresses.Api.DataTransferObjects;
using CommonLibrary.Messages;
using MediatR;

namespace Addresses.Api.App.Queries
{
    public sealed class GetByIdProvinceQuery
        : Command,
            IRequest<ProvinceDto>
    {
        public GetByIdProvinceQuery(
            string correlationToken,
            int id)
            : base(correlationToken)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
