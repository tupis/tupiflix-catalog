﻿namespace Tupi.Flix.Catalog.Domain.Entities
{
    public class Category
    {
        public Category(string name, string description)
        {
            Name = name;
            Description = description;
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            IsActive = true;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }
    }
}
