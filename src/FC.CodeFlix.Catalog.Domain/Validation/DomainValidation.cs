using FC.CodeFlix.Catalog.Domain.Exceptions;

namespace FC.CodeFlix.Catalog.Domain.Validation
{
    public class DomainValidation
    {

        public static void ShouldNotBeNull(string value, string fieldName)
        {
            if (value is null)
                throw new EntityValidationException($"{fieldName} should not be null");
        }
        public static void ShouldNotBeNullOrEmpty(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new EntityValidationException($"{fieldName} should not be null or empty");
        }
        public static void MinLenght(string value, int minLenght, string fieldName)
        {
            if (value.Length < minLenght)
                throw new EntityValidationException($"{fieldName} should not be less than {minLenght} characters long");
        }

        public static void MaxLenght(string value, int maxLenght, string fieldName)
        {
            if (value.Length > maxLenght)
                throw new EntityValidationException($"{fieldName} should not be greater than {maxLenght} characters long");
        }
    }
}
