using System.Collections.Generic;
using Addresses.Domain.SeedWork.Messages;
using MediatR;

namespace Addresses.Domain.SeedWork
{
    public abstract class Entity
        : INotification
    {
        private List<Event> _domainEvents = new List<Event>();
        private int? _requestedHashCode;

        public virtual int Id { get; protected set; }

        public IReadOnlyCollection<INotification> DomainEvents =>
            _domainEvents
                .AsReadOnly();

        public void AddDomainEvent(
            Event eventItem)
        {
            _domainEvents ??= new List<Event>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(
            Event eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public bool IsTransient()
        {
            return Id == default;
        }

        #region eq

        public override bool Equals(
            object obj)
        {
            if (obj == default ||
                !(obj is Entity))
                return false;

            if (ReferenceEquals(
                this,
                obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var item = (Entity) obj;

            if (item.IsTransient() ||
                IsTransient())
                return false;

            return item.Id == Id;
        }

        public override int GetHashCode()
        {
            if (IsTransient())
                return base
                    .GetHashCode();

            if (!_requestedHashCode.HasValue)
                _requestedHashCode =
                    Id.GetHashCode() ^
                    31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

            return _requestedHashCode
                .Value;
        }

        public static bool operator ==(
            Entity? left,
            Entity? right)
        {
            return Equals(
                left,
                null)
                ? Equals(
                    right,
                    null)
                : left.Equals(right);
        }

        public static bool operator !=(
            Entity? left,
            Entity? right)
        {
            return !(left == right);
        }

        #endregion
    }
}
