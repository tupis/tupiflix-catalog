using Bogus;
using Moq;
using Tupi.Flix.Catalog.Application.Interfaces;
using Tupi.Flix.Catalog.Application.UseCases.Category.CreateCategory;
using Tupi.Flix.Catalog.Domain.Repository;
using Tupi.Flix.Catalog.UnitTests.Common;

namespace Tupi.Flix.Catalog.UnitTests.Application.CreateCategory
{
    public class CreateCategoryMock : BaseMock
    {
        public string GetValidCategoryName()
        {
            string name = "";
            while (name.Length < 3)
            {
                name = Faker.Commerce.ProductName();
            }
            if (name.Length > 100) _ = name[..100];
            return name;
        }

        public string GetValidCategoryDescription()
        {
            string category = Faker.Commerce.ProductDescription();
            if (category.Length > 10_000) _ = category[..10_000];
            return category;
        }

        public static bool GetRandomBoolean() => new Random().NextDouble() > 0.5;

        public CreateCategoryInput CreateValidCategoryInput() => new(
            GetValidCategoryName(), 
            GetValidCategoryDescription(), 
            GetRandomBoolean()
        );

        public Mock<IUnitOfWork> CreateUnitOfWork => new();

        public Mock<ICategoryRepository> CreateRepository = new();
    }

    [CollectionDefinition(nameof(CreateCategoryMock))]
    public class CreateCategoryCollection : ICollectionFixture<CreateCategoryMock> {}
}
