using Domain.Abstractions;
using System;

namespace Domain.Primitives
{
    public abstract class Entity<TId> : IAuditableEntity
    {

        public TId Id { get; protected set; }

        protected Entity() { }

        protected Entity(TId id) 
        {
            Id = id;
        }

        public DateTime CreatedOnUtc { get; protected set; }

        public DateTime? ModifiedOnUtc { get; protected set; }

        public DateTime? DeletedOnUtc { get; protected set; }

        #region Comparações
        public override bool Equals(object obj)
        {
            if (obj is not Entity<TId> otherEntity)
                return false;

            return Id.Equals(otherEntity.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
        #endregion
    }
}
