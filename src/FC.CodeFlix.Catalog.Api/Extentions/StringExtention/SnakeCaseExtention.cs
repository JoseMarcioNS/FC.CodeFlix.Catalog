using Newtonsoft.Json.Serialization;

namespace FC.CodeFlix.Catalog.Api.Extentions.StringExtention
{
    public static class SnakeCaseExtention
    {
        private readonly static NamingStrategy _namingStrategy = new SnakeCaseNamingStrategy();
        public static string ToSnakeCase(this string stringToConvert)
        {
            ArgumentNullException.ThrowIfNull(stringToConvert, nameof(stringToConvert));
            return _namingStrategy.GetPropertyName(stringToConvert, false);
        }
    }
}
