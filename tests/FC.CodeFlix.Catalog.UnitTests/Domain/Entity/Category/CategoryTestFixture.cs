﻿using FC.CodeFlix.Catalog.UnitTests.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Domain.Entity.Category
{

    public class CategoryTestFixture : BaseFixture
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
        public DomainEntity.Category GetValidCategory() => new(GetValidName(),GetValidDescription());
    }

    [CollectionDefinition(nameof(CategoryTestFixture))]
    public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture> { }
}
