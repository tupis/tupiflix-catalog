namespace Tupi.Flix.Catalog.Domain.SeedWork
{
    public abstract class Entity
    {
        protected Entity() => Id = Guid.NewGuid();

        public Guid Id { get; set; }
    }
}
