using DomainCategoryEntity = Tupi.Flix.Catalog.Domain.Entities.Category;

namespace Tupi.Flix.Catalog.UnitTests.Domain.Entities.Category
{
    public class CategoryTestMock
    {
        public static DomainCategoryEntity Create()
        {
            return new DomainCategoryEntity("Category Name", "DescritionName");
        }
    }
}
