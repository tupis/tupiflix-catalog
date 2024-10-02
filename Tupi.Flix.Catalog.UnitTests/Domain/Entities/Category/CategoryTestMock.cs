using DomainCategoryEntity = Tupi.Flix.Catalog.Domain.Entities.Category;

namespace Tupi.Flix.Catalog.UnitTests.Domain.Entities.Category
{
    public class CategoryTestMock
    {
        public DomainCategoryEntity CreateValidCategory() => new("Category Name", "Descrition Name");
    }

    [CollectionDefinition(nameof(CategoryTestMock))]
    public class CategoryTestMockCollection : ICollectionFixture<CategoryTestMock> { }
}
