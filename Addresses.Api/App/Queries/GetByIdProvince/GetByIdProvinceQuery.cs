using CommonLibrary.Messages;
using MediatR;

namespace Addresses.Api.App.Queries.GetByIdProvince
{
    public sealed class GetByIdProvinceQuery
        : Command,
            IRequest<GetByIdProvinceDto?>
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
