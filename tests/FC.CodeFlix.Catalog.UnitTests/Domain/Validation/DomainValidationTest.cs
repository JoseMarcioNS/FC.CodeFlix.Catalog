using Bogus;
using FC.CodeFlix.Catalog.Domain.Exeptions;
using FC.CodeFlix.Catalog.Domain.Validation;
using FluentAssertions;

namespace FC.CodeFlix.Catalog.UnitTests.Domain.Validation
{
    public class DomainValidationTest
    {
        private Faker _faker = new Faker();

        [Fact(DisplayName = nameof(WhenNullThrowException))]
        [Trait("Domain", "Valitations - Validation")]
        public void WhenNullThrowException()
        {
            string fieldName = _faker.Commerce.ProductName();
            string? value = null;

            Action action = () => DomainValidation.ShouldNotBeNull(value, fieldName);

            action.Should().Throw<EntityValidationExeption>()
                .WithMessage($"{fieldName} should not be null");
        }
        [Fact(DisplayName = nameof(WhenNotbeNullOk))]
        [Trait("Domain", "Valitations - Validation")]
        public void WhenNotbeNullOk()
        {
            string fieldName = _faker.Commerce.ProductName();
            string? value = _faker.Commerce.ProductName();

            Action action = () => DomainValidation.ShouldNotBeNull(value, fieldName);

            action.Should().NotThrow();
        }
        [Theory(DisplayName = nameof(WhenNullOrEmptyThrowException))]
        [Trait("Domain", "Valitations - Validation")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void WhenNullOrEmptyThrowException(string? value)
        {
            string fieldName = _faker.Commerce.ProductName();

            Action action = () => DomainValidation.ShouldNotBeNullOrEmpty(value!, fieldName);

            action.Should().Throw<EntityValidationExeption>()
                .WithMessage($"{fieldName} should not be null or empty");
        }
        [Fact(DisplayName = nameof(WhenNotNetNullOrEmptyOk))]
        [Trait("Domain", "Valitations - Validation")]
        public void WhenNotNetNullOrEmptyOk()
        {
            string fieldName = _faker.Commerce.ProductName();
            string value = _faker.Commerce.ProductName();

            Action action = () => DomainValidation.ShouldNotBeNullOrEmpty(value, fieldName);

            action.Should().NotThrow();

        }
        [Theory(DisplayName = nameof(WhenLessThenMinLenghtThrowException))]
        [Trait("Domain", "Valitations - Validation")]
        [MemberData(nameof(GetValuesLessThanMin), parameters: 10)]
        public void WhenLessThenMinLenghtThrowException(string value, int minLenght)
        {
            string fieldName = _faker.Commerce.ProductName();

            Action action = () => DomainValidation.MinLenght(value, minLenght, fieldName);

            action.Should().Throw<EntityValidationExeption>()
                .WithMessage($"{fieldName} should not be less than {minLenght} characters long");

        }
        public static IEnumerable<object[]> GetValuesLessThanMin(int numberOfTest)
        {
            yield return new object[] { "12345", 6 };

            Faker faker = new Faker();

            for (int i = 0; i < numberOfTest; i++)
            {
                var value = faker.Commerce.ProductName();
                var minLenght = value.Length + new Random().Next(1, 10);
                yield return new object[] { value, minLenght };
            }
        }
        [Theory(DisplayName = nameof(WhenLessThenMinLenghtThrowException))]
        [Trait("Domain", "Valitations - Validation")]
        [MemberData(nameof(GetValuesGreaterThanMin), parameters: 10)]
        public void WhenGreaterThenMinLenghtOK(string value, int minLenght)
        {
            string fieldName = _faker.Commerce.ProductName();

            Action action = () => DomainValidation.MinLenght(value, minLenght, fieldName);

            action.Should().NotThrow();

        }
        public static IEnumerable<object[]> GetValuesGreaterThanMin(int numberOfTest)
        {
            yield return new object[] { "12345", 5 };

            Faker faker = new Faker();

            for (int i = 0; i < numberOfTest; i++)
            {
                var value = faker.Commerce.ProductName();
                var minLenght = value.Length - new Random().Next(1, 10);
                yield return new object[] { value, minLenght };
            }
        }
        [Theory(DisplayName = nameof(WhenGreaterThanMaxLenghtThrowException))]
        [Trait("Domain", "Valitations - Validation")]
        [MemberData(nameof(GetValuesGreaterThanMax), parameters: 10)]
        public void WhenGreaterThanMaxLenghtThrowException(string value, int maxLenght)
        {
            string fieldName = _faker.Commerce.ProductName();

            Action action = () => DomainValidation.MaxLenght(value, maxLenght, fieldName);

            action.Should().Throw<EntityValidationExeption>()
                .WithMessage($"{fieldName} should not be greater than {maxLenght} characters long");

        }
        public static IEnumerable<object[]> GetValuesGreaterThanMax(int numberOfTest)
        {
            yield return new object[] { "1234567", 6 };

            Faker faker = new Faker();

            for (int i = 0; i < numberOfTest; i++)
            {
                var value = faker.Commerce.ProductName();
                var maxLenght = value.Length - new Random().Next(1, 10);
                yield return new object[] { value, maxLenght };
            }
        }
        [Theory(DisplayName = nameof(WhenLessThanMaxLenghtOk))]
        [Trait("Domain", "Valitations - Validation")]
        [MemberData(nameof(GetValuesLessThanMax), parameters: 10)]
        public void WhenLessThanMaxLenghtOk(string value, int maxLenght)
        {
            string fieldName = _faker.Commerce.ProductName();

            Action action = () => DomainValidation.MaxLenght(value, maxLenght, fieldName);

            action.Should().NotThrow();

        }
        public static IEnumerable<object[]> GetValuesLessThanMax(int numberOfTest)
        {
            yield return new object[] { "1234567", 7 };

            Faker faker = new Faker();

            for (int i = 0; i < numberOfTest; i++)
            {
                var value = faker.Commerce.ProductName();
                var maxLenght = value.Length + new Random().Next(1, 10);
                yield return new object[] { value, maxLenght };
            }
        }
    }
}
