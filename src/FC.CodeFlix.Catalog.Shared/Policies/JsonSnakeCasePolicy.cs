using FC.CodeFlix.Catalog.Shared.Extentions.StringExtention;
using System.Text.Json;

namespace FC.CodeFlix.Catalog.Shared.Policies
{
    public class JsonSnakeCasePolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        => name.ToSnakeCase();
    }
}
