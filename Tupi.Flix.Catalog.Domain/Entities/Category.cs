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
            ValidateName();
            ValidateDescription();
        }

        private void ValidateName()
        {
            if (String.IsNullOrWhiteSpace(Name))
            {
                throw new EntityValidationException($"{nameof(Name)} should not be empty or null");
            }

            if (Name.Length < 3)
            {
                throw new EntityValidationException($"{nameof(Name)} not should be less than 3 character");
            }

            if (Name.Length > 100)
            {
                throw new EntityValidationException($"{nameof(Name)} not should be more than 100 character");
            }
        }

        public void Activate()
        {
            IsActive = true;
            Validate();
        }

        public void Dectivate()
        {
            IsActive = false;
            Validate();
        }

        private void ValidateDescription()
        {
            if (Description == null)
            {
                throw new EntityValidationException($"{nameof(Description)} should not be null");
            }
            
            if(Description.Length > 10000)
            {
                throw new EntityValidationException($"{nameof(Description)} should be less than 10000 character");
            }
        }
    }
}
