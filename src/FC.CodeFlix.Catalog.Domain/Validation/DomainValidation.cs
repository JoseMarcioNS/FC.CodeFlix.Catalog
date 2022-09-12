using FC.CodeFlix.Catalog.Domain.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FC.CodeFlix.Catalog.Domain.Validation
{
    public class DomainValidation
    {

        public static void ShouldNotBeNull(string value, string fieldName)
        {
            if (value is null)
                throw new EntityValidationExeption($"{fieldName} should not be null");
        }
        public static void ShouldNotBeNullOrEmpty(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new EntityValidationExeption($"{fieldName} should not be null or empty");
        }
        public static void MinLenght(string value, int minLenght, string fieldName)
        {
            if (value.Length < minLenght)
                throw new EntityValidationExeption($"{fieldName} should not be less than {minLenght} characters long");
        }

        public static void MaxLenght(string value, int maxLenght, string fieldName)
        {
            if (value.Length > maxLenght)
                throw new EntityValidationExeption($"{fieldName} should not be greater than {maxLenght} characters long");
        }
    }
}
