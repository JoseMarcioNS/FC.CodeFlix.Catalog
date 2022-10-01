using System.Text.Json;

namespace FC.CodeFlix.Catalog.Api.Extentions.StringExtention
{
    public class JsonSnakeCasePolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        => name.ToSnakeCase();
    }
}
