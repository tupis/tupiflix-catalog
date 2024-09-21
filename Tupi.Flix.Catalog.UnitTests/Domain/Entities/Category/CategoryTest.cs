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

            Assert.NotNull(category);
            Assert.Equal(validData.Name, category.Name);
            Assert.Equal(validData.Description, category.Description);
            Assert.NotEqual(default(Guid), category.Id);
            Assert.NotEqual(default(DateTime), category.CreatedAt);
            Assert.True(category.CreatedAt > dateTimeBefore);
            Assert.True(category.CreatedAt < dateTimeAfter);
            Assert.True(category.IsActive);
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

            Assert.NotNull(category);
            Assert.Equal(validData.Name, category.Name);
            Assert.Equal(validData.Description, category.Description);
            Assert.NotEqual(default(Guid), category.Id);
            Assert.NotEqual(default(DateTime), category.CreatedAt);
            Assert.True(category.CreatedAt > dateTimeBefore);
            Assert.True(category.CreatedAt < dateTimeAfter);
            Assert.Equal(category.IsActive, isActive);
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

            var category = new DomainCategoryEntity(validData.Name, validData.Description, false);
            Assert.True(!category.IsActive);
            category.Activate();
            Assert.True(category.IsActive);

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
            Assert.True(category.IsActive);
            category.Dectivate();
            Assert.True(!category.IsActive);

        }

        [Theory(DisplayName = nameof(ThrowErrorWhenNameIsEmpty))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void ThrowErrorWhenNameIsEmpty(string? nameCategory)
        {
            Action action = () => new DomainCategoryEntity(nameCategory!, "category description");
            var expection = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Name should not be empty or null", expection.Message);
        }

        [Fact(DisplayName = nameof(ThrowErrorWhenDescriptionIsNull))]
        [Trait("Domain", "Category - Aggregates")]
        public void ThrowErrorWhenDescriptionIsNull()
        {
            Action action = () => new DomainCategoryEntity("Name Category", null!);
            var expection = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Description should not be null", expection.Message);
        }

        [Theory(DisplayName = nameof(ThrowErrorWhenNameWithLessThreeChar))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("1")]
        public void ThrowErrorWhenNameWithLessThreeChar(string invalidName) {
            Action action = () => new DomainCategoryEntity(invalidName, "Description Category");
            var exeption = Assert .Throws<EntityValidationException>(()=> action());
            Assert.Equal("Name not should be less than 3 character", exeption.Message);
        } 

        [Fact(DisplayName = nameof(ThrowErrorWhenNameWithMoreOneHundredChar))]
        [Trait("Domain", "Category - Aggregates")]
        public void ThrowErrorWhenNameWithMoreOneHundredChar()
        {
            string invalidName = RandomChar(100);
            Action action = () => new DomainCategoryEntity(invalidName, "Description Category");
            Console.WriteLine(invalidName);
            var exeption = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Name not should be more than 100 character", exeption.Message);
        }

        [Fact(DisplayName = nameof(ThrowErrorWhenDescriptionWithMoreOneThousandChar))]
        [Trait("Domain", "Category - Aggregates")]
        public void ThrowErrorWhenDescriptionWithMoreOneThousandChar()
        {
            string invalidDescription = RandomChar(10_000);
            Action action = () => new DomainCategoryEntity("Name category", invalidDescription);
            var exeption = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Description should be less than 10000 character", exeption.Message);
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

            Assert.Equal(category.Name, updatedData.Name);
            Assert.Equal(category.Description, updatedData.Description);
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

            Assert.Equal(category.Name, updatedData.Name);
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

            Assert.Equal(category.Description, updatedData.Description);
        }

        [Theory(DisplayName = nameof(ThrowErrorWhenUpdateNameWithLessThreeChar))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("1")]
        [InlineData("12")]
        public void ThrowErrorWhenUpdateNameWithLessThreeChar(string invalidName)
        {
            DomainCategoryEntity category = new("Valida name", "decription category");
            void action() => category.Update(invalidName);
            EntityValidationException exception = Assert.Throws<EntityValidationException>(action);
            Assert.Equal("Name not should be less than 3 character", exception.Message);
        }

        [Fact(DisplayName = nameof(ThrowErrorWhenUpdateNameWithMoreOneHundredChar))]
        [Trait("Domain", "Category - Aggregates")]
        public void ThrowErrorWhenUpdateNameWithMoreOneHundredChar()
        {
            string invalidName = RandomChar(100);
            DomainCategoryEntity category = new("Valida name", "decription category");
            void action() => category.Update(invalidName);
            EntityValidationException exception = Assert.Throws<EntityValidationException>(action);
            Assert.Equal("Name not should be more than 100 character", exception.Message);
        }

        [Fact(DisplayName = nameof(ThrowErrorWhenUpdateDescriptionWithMoreOneThousandChar))]
        [Trait("Domain", "Category - Aggregates")]
        public void ThrowErrorWhenUpdateDescriptionWithMoreOneThousandChar()
        {
            string invalidDescription = RandomChar(10_000);
            DomainCategoryEntity category = new("Name category", "valid description");
            Action action = () => category.Update(null, invalidDescription);
            EntityValidationException exception = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Description should be less than 10000 character", exception.Message);
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
