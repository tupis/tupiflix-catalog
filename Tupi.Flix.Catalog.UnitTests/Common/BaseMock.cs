using Bogus;

namespace Tupi.Flix.Catalog.UnitTests.Common
{
    public abstract class BaseMock
    {
        protected BaseMock() => Faker = new Faker("pt_BR");

        public Faker Faker { get; set; }
    }
}
