using FC.CodeFlix.Catalog.Api.Extentions.StringExtention;
using System.Text.Json;

namespace FC.CodeFlix.Catalog.Api.Extentions.Policies
{
    public class JsonSnakeCasePolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        => name.ToSnakeCase();
    }
}
