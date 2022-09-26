using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.SharedTests.Common;

namespace FC.CodeFlix.Catalog.SharedTests
{
    public class CategoryBaseFixture : BaseFixture
    {
        public string GetValidName()
        {
            var name = "";
            while (name.Length < 3)
                name = Faker.Commerce.Categories(1)[0];

            if (name.Length > 255)
                name = name[..255];

            return name;
        }
        public string GetValidDescription()
        {
            var description = Faker.Commerce.ProductDescription();
            if (description.Length > 10_000)
                description = description[..10_000];

            return description;
        }
        public bool GetRandomActive() => new Random().NextDouble() > 0.5;
        public Category GetValidCategory()
           => new(GetValidName(),
                  GetValidDescription(),
                  GetRandomActive());

        public List<Category> GetListCategories(int length = 10)
                 => Enumerable.Range(1, length)
                       .Select(_ => GetValidCategory()).ToList();
        public string GetInvalidNameMinLenght()
        => Faker.Commerce.ProductName()[..2];
        public string GetInvalidNameMaxLeght()
        {
            var input = Faker.Commerce.ProductName();
            while (input.Length <= 255)
                input += Faker.Commerce.ProductName();
            return input;
        }
        public string GetInvalidDescriptionMaxLeght()
        {
            var description = Faker.Commerce.ProductDescription();

            while (description.Length <= 10_000)
                description += Faker.Commerce.ProductDescription();
            return description;
        }


    }
}
