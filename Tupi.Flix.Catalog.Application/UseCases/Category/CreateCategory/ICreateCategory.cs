namespace Tupi.Flix.Catalog.Application.UseCases.Category.CreateCategory
{
    public interface ICreateCategory
    {
        public Task<CreateCategoryOutput> Execute(CreateCategoryInput input, CancellationToken cancellationToken);
    }
}
