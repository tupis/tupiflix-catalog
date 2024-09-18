namespace Tupi.Flix.Catalog.Domain.Execeptions
{
    public class EntityValidationException : Exception
    {
        public EntityValidationException(string? message) : base(message)
        {
        }
    }
}
