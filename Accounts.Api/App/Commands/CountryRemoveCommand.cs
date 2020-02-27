using System.Collections.Generic;
using CommonLibrary.Messages;

namespace Accounts.Api.App.Commands
{
    public sealed class CountryRemoveCommand
        : Command
    {
        public CountryRemoveCommand(
            string correlationToken,
            int id,
            IEnumerable<int> provinceIds)
            : base(correlationToken)
        {
            Id = id;
            ProvinceIds = provinceIds;
        }

        public int Id { get; }
        public IEnumerable<int> ProvinceIds { get; }
    }
}
