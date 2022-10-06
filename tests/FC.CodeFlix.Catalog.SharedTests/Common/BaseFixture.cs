using Bogus;
using FC.CodeFlix.Catalog.Domain.Entity;

namespace FC.CodeFlix.Catalog.SharedTests.Common
{
    public class BaseFixture
    {
        public Faker Faker { get; set; }
        public BaseFixture()
             => Faker = new Faker("pt_BR");

        public bool GetRandomActive() => new Random().NextDouble() > 0.5;
    }
}
