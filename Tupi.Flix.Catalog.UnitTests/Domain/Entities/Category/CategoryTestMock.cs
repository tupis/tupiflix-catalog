using System.Xml;
using Tupi.Flix.Catalog.UnitTests.Domain.Common;
using DomainCategoryEntity = Tupi.Flix.Catalog.Domain.Entities.Category;

namespace Tupi.Flix.Catalog.UnitTests.Domain.Entities.Category
{
    public class CategoryTestMock : BaseMock
    {
        public CategoryTestMock() : base() { }

        public string GetValidCategoryName()
        {
            string categoryName = "";
            while (categoryName.Length < 3 )
            {
                categoryName = Faker.Commerce.Categories(1)[0];
            }

            if (categoryName.Length > 100) _ = categoryName[..100];

            return categoryName;
        }

        public string GetValidCategoryDescription()
        {
            string categoryDescription = Faker.Commerce.ProductDescription();

            if (categoryDescription.Length > 10_000) _ = categoryDescription[..10_000];

            return categoryDescription;
        }

        public DomainCategoryEntity CreateValidCategory() => new(GetValidCategoryName(), GetValidCategoryDescription());
    }

    [CollectionDefinition(nameof(CategoryTestMock))]
    public class CategoryTestMockCollection : ICollectionFixture<CategoryTestMock> { }
}
