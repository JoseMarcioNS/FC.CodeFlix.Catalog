using Bogus;
namespace FC.CodeFlix.Catalog.IntegrationTests.Common
{
    public class BaseFixture
    {
        public Faker Faker { get; set; }
        public BaseFixture()
               => Faker = new Faker("pt_BR");

    }
}
