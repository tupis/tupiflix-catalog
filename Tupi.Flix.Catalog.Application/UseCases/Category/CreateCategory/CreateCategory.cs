using Tupi.Flix.Catalog.Application.Interfaces;
using CategoryEntity = Tupi.Flix.Catalog.Domain.Entities.Category;
using Tupi.Flix.Catalog.Domain.Repository;

namespace Tupi.Flix.Catalog.Application.UseCases.Category.CreateCategory
{
    public class CreateCategory(ICategoryRepository repository, IUnitOfWork unitOfWork) : ICreateCategory
    {

        private readonly ICategoryRepository _respository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CreateCategoryOutput> Execute(CreateCategoryInput input, CancellationToken cancellationToken)
        {
            var category = new CategoryEntity(
                input.Name, 
                input.Description,
                input.IsActive
            );

            await _respository.Insert(category, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return new CreateCategoryOutput(
                category.Id, 
                category.Name, 
                category.Description, 
                category.IsActive,
                category.CreatedAt
            );
        }
    }
}
