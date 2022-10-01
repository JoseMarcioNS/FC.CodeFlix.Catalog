using System.Text.Json;

namespace FC.CodeFlix.Catalog.Shared.Policies
{
    public static class JsonSerializerOptionsPolicies
    {
        public static JsonSerializerOptions SetJsonSerializerOptionsPolicies()
        {
            return new JsonSerializerOptions
            {
                PropertyNamingPolicy = new JsonSnakeCasePolicy(),
                PropertyNameCaseInsensitive = true,
            };
        }
    }
}
