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
    }
}
