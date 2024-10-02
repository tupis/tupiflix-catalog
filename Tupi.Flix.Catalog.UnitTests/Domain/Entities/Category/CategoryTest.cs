using FluentAssertions;
using Microsoft.VisualBasic;
using Tupi.Flix.Catalog.Domain.Execeptions;
using DomainCategoryEntity = Tupi.Flix.Catalog.Domain.Entities.Category;

namespace Tupi.Flix.Catalog.UnitTests.Domain.Entities.Category
{
    [Collection(nameof(CategoryTestMock))]
    public class CategoryTest(CategoryTestMock categoryMock)
    {
        private readonly CategoryTestMock _categoryMock = categoryMock;

        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Instantiate()
        {
            var validData = _categoryMock.CreateValidCategory();

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
            var validData = _categoryMock.CreateValidCategory();

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
            var validData = _categoryMock.CreateValidCategory();

            DomainCategoryEntity category = new(validData.Name, validData.Description, false);
            category.IsActive.Should().BeFalse();
            category.Activate();
            category.IsActive.Should().BeTrue();
        }

        [Fact(DisplayName = nameof(DeactivateCategory))]
        [Trait("Domain", "Category - Aggregates")]
        public void DeactivateCategory()
        {
            var validData = _categoryMock.CreateValidCategory();

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
        [MemberData(nameof(GetNamesWithLessThreeChar), parameters: 10)]
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
            string invalidName = _categoryMock.Faker.Lorem.Letter(102);
            Action action = () => new DomainCategoryEntity(invalidName, "Description Category");
            action.Should()
               .Throw<EntityValidationException>()
               .WithMessage("Name not should be more than 100 character");
        }

        [Fact(DisplayName = nameof(ThrowErrorWhenDescriptionWithMoreOneThousandChar))]
        [Trait("Domain", "Category - Aggregates")]
        public void ThrowErrorWhenDescriptionWithMoreOneThousandChar()
        {
            string invalidDescription = _categoryMock.Faker.Lorem.Letter(10_001); ;
            Action action = () => new DomainCategoryEntity("Name category", invalidDescription);
            action.Should()
               .Throw<EntityValidationException>()
               .WithMessage("Description should be less than 10000 character");
        }

        [Fact(DisplayName = nameof(Update))]
        [Trait("Domain", "Category - Aggregates")]
        public void Update()
        {
            var createData = _categoryMock.CreateValidCategory();

            DomainCategoryEntity category = new(createData.Name, createData.Description);

            var updatedData = _categoryMock.CreateValidCategory();

            category.Update(updatedData.Name, updatedData.Description);
            category.Name.Should().Be(updatedData.Name);
            category.Description.Should().Be(updatedData.Description);
        }

        [Fact(DisplayName = nameof(UpdateOnlyName))]
        [Trait("Domain", "Category - Aggregates")]
        public void UpdateOnlyName()
        {
            var createData = _categoryMock.CreateValidCategory();

            DomainCategoryEntity category = new(createData.Name, createData.Description);

            var updatedData = _categoryMock.CreateValidCategory();

            category.Update(updatedData.Name);
            category.Name.Should().Be(updatedData.Name);
        }

        [Fact(DisplayName = nameof(UpdateOnlyDesctiption))]
        [Trait("Domain", "Category - Aggregates")]
        public void UpdateOnlyDesctiption()
        {
            DomainCategoryEntity category = _categoryMock.CreateValidCategory();

            var updatedData = _categoryMock.CreateValidCategory();

            category.Update(null, updatedData.Description);
            category.Description.Should().Be(updatedData.Description);
        }

        [Theory(DisplayName = nameof(ThrowErrorWhenUpdateNameWithLessThreeChar))]
        [Trait("Domain", "Category - Aggregates")]
        [MemberData(nameof(GetNamesWithLessThreeChar), parameters: 10)]
        public void ThrowErrorWhenUpdateNameWithLessThreeChar(string invalidName)
        {
            DomainCategoryEntity category = _categoryMock.CreateValidCategory();
            Action action = () => category.Update(invalidName);
            action.Should()
               .Throw<EntityValidationException>()
               .WithMessage("Name not should be less than 3 character");
        }

        [Fact(DisplayName = nameof(ThrowErrorWhenUpdateNameWithMoreOneHundredChar))]
        [Trait("Domain", "Category - Aggregates")]
        public void ThrowErrorWhenUpdateNameWithMoreOneHundredChar()
        {
            string invalidName = _categoryMock.Faker.Lorem.Letter(102);
            DomainCategoryEntity category = _categoryMock.CreateValidCategory();
            Action action = () => category.Update(invalidName);
            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name not should be more than 100 character");
        }

        [Fact(DisplayName = nameof(ThrowErrorWhenUpdateDescriptionWithMoreOneThousandChar))]
        [Trait("Domain", "Category - Aggregates")]
        public void ThrowErrorWhenUpdateDescriptionWithMoreOneThousandChar()
        {
            string invalidDescription = _categoryMock.Faker.Lorem.Letter(10_001);
            DomainCategoryEntity category = _categoryMock.CreateValidCategory();
            Action action = () => category.Update(null, invalidDescription);
            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Description should be less than 10000 character");
        }

        public static IEnumerable<object[]> GetNamesWithLessThreeChar(int numberOfTest = 6)
        {
            var mock = new CategoryTestMock();

            for (int i = 0; i < numberOfTest; i++)
            {
                bool isOdd = i % 2 == 0;
                yield return new object[] { mock.GetValidCategoryName()[..(isOdd ? 1 : 2)] };
            }
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
