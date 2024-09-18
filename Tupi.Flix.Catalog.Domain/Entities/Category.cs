using Tupi.Flix.Catalog.Domain.Execeptions;

namespace Tupi.Flix.Catalog.Domain.Entities
{
    public class Category
    {
        public Category(string name, string description, bool isActive = true)
        {
            Name = name;
            Description = description;
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            IsActive = isActive;

            Validate();
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }

        public void Validate()
        {
            if (String.IsNullOrWhiteSpace(Name))
            {
                throw new EntityValidationException($"{nameof(Name)} should not be empty or null");
            }

            if (Description == null)
            {
                throw new EntityValidationException($"{nameof(Description)} should not be null");
            }
        }
    }
}
