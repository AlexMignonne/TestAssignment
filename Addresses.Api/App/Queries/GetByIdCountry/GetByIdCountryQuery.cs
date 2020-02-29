using CommonLibrary.Messages;
using MediatR;

namespace Addresses.Api.App.Queries.GetByIdCountry
{
    public sealed class GetByIdCountryQuery
        : Command,
            IRequest<GetByIdCountryDto?>
    {
        public GetByIdCountryQuery(
            string correlationToken,
            int id)
            : base(correlationToken)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
