using BNA.EF1.Domain.Common.Interfaces;

namespace BNA.EF1.Domain.Common
{
    public abstract class Entity
    {
        protected readonly List<IDomainEvent> _domainEvents = new();

        protected Entity(Guid? id) => Id = id ?? Guid.NewGuid();

        public Guid Id { get; init; }

        public List<IDomainEvent> PopDomainEvents()
        {
            var copy = _domainEvents.ToList();
            _domainEvents.Clear();

            return copy;
        }

        protected Entity() { }
    }
}
