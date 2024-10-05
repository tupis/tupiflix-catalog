using FluentAssertions;
using Moq;
using Tupi.Flix.Catalog.Application.Interfaces;
using Tupi.Flix.Catalog.Application.UseCases.Category;
using Tupi.Flix.Catalog.Domain.Entities;
using Tupi.Flix.Catalog.Domain.Repository;
using UseCasesCreateCategory = Tupi.Flix.Catalog.Application.UseCases.Category.CreateCategory;

namespace Tupi.Flix.Catalog.UnitTests.Application.CreateCategory
{
    [Collection(nameof(CreateCategoryMock))]
    public class CreateCategoryTest(CreateCategoryMock mock)
    {

        CreateCategoryMock _mock = mock;

        [Fact(DisplayName = nameof(CreateCategory))]
        [Trait("Application", "Create Category - Use Cases")]
        public async void CreateCategory()
        {
            var expectedCategory = CreateMockCategoryEntity();
            var repositoryMock = _mock.CreateRepository;
            var unitOfWorkMock = _mock.CreateUnitOfWork;
            var useCase = new UseCasesCreateCategory.CreateCategory(repositoryMock.Object, unitOfWorkMock.Object);
            var input = new UseCasesCreateCategory.CreateCategoryInput(expectedCategory.Name, expectedCategory.Description, expectedCategory.IsActive);
            var output = await useCase.Execute(input, CancellationToken.None);

            repositoryMock.Verify(
                repository => repository.Insert(
                    It.IsAny<Category>(),
                    It.IsAny<CancellationToken>()
                ), 
                Times.Once
            );

            unitOfWorkMock.Verify(
                unitOfWork => unitOfWork.Commit(
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );

            output.Should().NotBeNull();
            output.Name.Should().Be(expectedCategory.Name);
            output.Description.Should().Be(expectedCategory.Description);
            output.IsActive.Should().Be(expectedCategory.IsActive);
        }

        private static Category CreateMockCategoryEntity() => new("Category Name", "Category Description", true);
    }
}
