using CommonLibrary.Messages;
using MediatR;

namespace Addresses.Api.App.Queries
{
    public sealed class IsExistProvinceByIdQuery
        : Command,
            IRequest<bool>
    {
        public IsExistProvinceByIdQuery(
            string correlationToken,
            int id)
            : base(correlationToken)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
