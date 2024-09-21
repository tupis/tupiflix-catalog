using FluentAssertions;
using Tupi.Flix.Catalog.Domain.Execeptions;
using DomainCategoryEntity = Tupi.Flix.Catalog.Domain.Entities.Category;

namespace Tupi.Flix.Catalog.UnitTests.Domain.Entities.Category
{
    public class CategoryTest
    {
        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Instantiate()
        {
            var validData = new
            {
                Name = "category name",
                Description = "category description",
            };

            var dateTimeBefore = DateTime.Now;
            var category = new DomainCategoryEntity(validData.Name, validData.Description);
            var dateTimeAfter = DateTime.Now;

            category.Should().NotBeNull();
            category.Name.Should().Be(validData.Name);
            category.Description.Should().Be(validData.Description);
            category.Id.Should().NotBe(default(Guid));
            category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
            (category.CreatedAt > dateTimeBefore).Should().BeTrue();
            (category.CreatedAt < dateTimeAfter).Should().BeTrue();
            category.IsActive.Should().BeTrue();
        }

        [Theory(DisplayName = nameof(InstantiateWithIsActiveStatus))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(true)]
        [InlineData(false)]
        public void InstantiateWithIsActiveStatus(bool isActive)
        {
            var validData = new
            {
                Name = "category name",
                Description = "category description",
            };

            var dateTimeBefore = DateTime.Now;
            var category = new DomainCategoryEntity(validData.Name, validData.Description, isActive);
            var dateTimeAfter = DateTime.Now;

            category.Should().NotBeNull();
            category.Name.Should().Be(validData.Name);
            category.Description.Should().Be(validData.Description);
            category.Id.Should().NotBe(default(Guid));
            category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
            (category.CreatedAt > dateTimeBefore).Should().BeTrue();
            (category.CreatedAt < dateTimeAfter).Should().BeTrue();
            category.IsActive.Should().Be(isActive);
        }

        [Fact(DisplayName = nameof(ActivateCategory))]
        [Trait("Domain", "Category - Aggregates")]
        public void ActivateCategory()
        {
            var validData = new
            {
                Name = "category name",
                Description = "category description",
            };

            DomainCategoryEntity category = new(validData.Name, validData.Description, false);
            category.IsActive.Should().BeFalse();
            category.Activate();
            category.IsActive.Should().BeTrue();
        }

        [Fact(DisplayName = nameof(DeactivateCategory))]
        [Trait("Domain", "Category - Aggregates")]
        public void DeactivateCategory()
        {
            var validData = new
            {
                Name = "category name",
                Description = "category description",
            };

            var category = new DomainCategoryEntity(validData.Name, validData.Description, true);
            category.IsActive.Should().BeTrue();
            category.Dectivate();
            category.IsActive.Should().BeFalse();
        }

        [Theory(DisplayName = nameof(ThrowErrorWhenNameIsEmpty))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void ThrowErrorWhenNameIsEmpty(string? nameCategory)
        {
            Action action = () => new DomainCategoryEntity(nameCategory!, "category description");
            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should not be empty or null");
        }

        [Fact(DisplayName = nameof(ThrowErrorWhenDescriptionIsNull))]
        [Trait("Domain", "Category - Aggregates")]
        public void ThrowErrorWhenDescriptionIsNull()
        {
            Action action = () => new DomainCategoryEntity("Name Category", null!);
            action.Should()
               .Throw<EntityValidationException>()
               .WithMessage("Description should not be null");
        }

        [Theory(DisplayName = nameof(ThrowErrorWhenNameWithLessThreeChar))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("1")]
        public void ThrowErrorWhenNameWithLessThreeChar(string invalidName) {
            Action action = () => new DomainCategoryEntity(invalidName, "Description Category");
            action.Should()
               .Throw<EntityValidationException>()
               .WithMessage("Name not should be less than 3 character");
        } 

        [Fact(DisplayName = nameof(ThrowErrorWhenNameWithMoreOneHundredChar))]
        [Trait("Domain", "Category - Aggregates")]
        public void ThrowErrorWhenNameWithMoreOneHundredChar()
        {
            string invalidName = RandomChar(100);
            Action action = () => new DomainCategoryEntity(invalidName, "Description Category");
            action.Should()
               .Throw<EntityValidationException>()
               .WithMessage("Name not should be more than 100 character");
        }

        [Fact(DisplayName = nameof(ThrowErrorWhenDescriptionWithMoreOneThousandChar))]
        [Trait("Domain", "Category - Aggregates")]
        public void ThrowErrorWhenDescriptionWithMoreOneThousandChar()
        {
            string invalidDescription = RandomChar(10_000);
            Action action = () => new DomainCategoryEntity("Name category", invalidDescription);
            action.Should()
               .Throw<EntityValidationException>()
               .WithMessage("Description should be less than 10000 character");
        }

        [Fact(DisplayName = nameof(Update))]
        [Trait("Domain", "Category - Aggregates")]
        public void Update()
        {
            var createData = new
            {
                Name = "New category name",
                Description = "New category description",
            };

            DomainCategoryEntity category = new(createData.Name, createData.Description);

            var updatedData = new
            {
                Name = "Updated category name",
                Description = "Updated category description",
            };

            category.Update(updatedData.Name, updatedData.Description);
            category.Name.Should().Be(updatedData.Name);
            category.Description.Should().Be(updatedData.Description);
        }

        [Fact(DisplayName = nameof(UpdateOnlyName))]
        [Trait("Domain", "Category - Aggregates")]
        public void UpdateOnlyName()
        {
            var createData = new
            {
                Name = "New category name",
                Description = "New category description",
            };

            DomainCategoryEntity category = new DomainCategoryEntity(createData.Name, createData.Description);

            var updatedData = new
            {
                Name = "Updated category name",
            };

            category.Update(updatedData.Name);
            category.Name.Should().Be(updatedData.Name);
        }

        [Fact(DisplayName = nameof(UpdateOnlyDesctiption))]
        [Trait("Domain", "Category - Aggregates")]
        public void UpdateOnlyDesctiption()
        {
            var createData = new
            {
                Name = "New category name",
                Description = "New category description",
            };

            DomainCategoryEntity category = new DomainCategoryEntity(createData.Name, createData.Description);

            var updatedData = new
            {
                Description = "Updated category description",
            };

            category.Update(null, updatedData.Description);
            category.Description.Should().Be(updatedData.Description);
        }

        [Theory(DisplayName = nameof(ThrowErrorWhenUpdateNameWithLessThreeChar))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("1")]
        [InlineData("12")]
        public void ThrowErrorWhenUpdateNameWithLessThreeChar(string invalidName)
        {
            DomainCategoryEntity category = new("Valida name", "decription category");
            Action action = () => category.Update(invalidName);
            action.Should()
               .Throw<EntityValidationException>()
               .WithMessage("Name not should be less than 3 character");
        }

        [Fact(DisplayName = nameof(ThrowErrorWhenUpdateNameWithMoreOneHundredChar))]
        [Trait("Domain", "Category - Aggregates")]
        public void ThrowErrorWhenUpdateNameWithMoreOneHundredChar()
        {
            string invalidName = RandomChar(100);
            DomainCategoryEntity category = new("Valida name", "decription category");
            Action action = () => category.Update(invalidName);
            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name not should be more than 100 character");
        }

        [Fact(DisplayName = nameof(ThrowErrorWhenUpdateDescriptionWithMoreOneThousandChar))]
        [Trait("Domain", "Category - Aggregates")]
        public void ThrowErrorWhenUpdateDescriptionWithMoreOneThousandChar()
        {
            string invalidDescription = RandomChar(10_000);
            DomainCategoryEntity category = new("Name category", "valid description");
            Action action = () => category.Update(null, invalidDescription);
            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Description should be less than 10000 character");
        }

        private static string RandomChar(int minLength)
        {
            int random = new Random().Next(1, 10);
            int MIN_RANGE = minLength;
            int MAX_RANGE = MIN_RANGE * random;
            return String.Join(null, Enumerable.Range(MIN_RANGE, MAX_RANGE).ToArray());
        }
    }
}
