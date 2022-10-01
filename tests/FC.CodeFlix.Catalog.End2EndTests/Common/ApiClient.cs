using FC.CodeFlix.Catalog.Api.Extentions.Policies;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Json;

namespace FC.CodeFlix.Catalog.End2EndTests.Common
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializerOptions;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _serializerOptions = new()
            {
                PropertyNamingPolicy = new JsonSnakeCasePolicy(),
                PropertyNameCaseInsensitive = true
            };
        }
      public async Task<(HttpResponseMessage?, TOutput?)> Post<TOutput>(
            string route,
            object payload
            ) where TOutput : class
        {
            var response = await _httpClient.PostAsync(
                route,
                new StringContent(
                     JsonSerializer.Serialize(payload,
                     _serializerOptions),
                     Encoding.UTF8,
                     "application/json"
                    )
                );
            TOutput? output = await GetOutpu<TOutput>(response);

            return (response, output);
        }
        public async Task<(HttpResponseMessage?, TOutput?)> Get<TOutput>(
           string route,
           object? queryStringParametersObject = null
            ) where TOutput : class
        {
            var url = PrepareRoute(route, queryStringParametersObject);
            var response = await _httpClient.GetAsync(url);
            TOutput? output = await GetOutpu<TOutput>(response);
            return (response, output);
        }

        public async Task<(HttpResponseMessage?, TOutput?)> Delete<TOutput>(string route)
            where TOutput : class
        {
            var response = await _httpClient.DeleteAsync(route);
            TOutput? output = await GetOutpu<TOutput>(response);

            return (response, output);
        }
        public async Task<(HttpResponseMessage?, TOutput?)> Put<TOutput>(
            string route,
            object payload
            ) where TOutput : class
        {
            var response = await _httpClient.PutAsync(
                route,
                new StringContent(
                     JsonSerializer.Serialize(payload,
                     _serializerOptions),
                     Encoding.UTF8,
                     "application/json"
                    )
                );

            TOutput? output = await GetOutpu<TOutput>(response);

            return (response, output);
        }
        private async Task<TOutput?> GetOutpu<TOutput>(HttpResponseMessage response) where TOutput : class
        {
            var outputString = await response.Content.ReadAsStringAsync();
            TOutput? output = null;
            if (!string.IsNullOrWhiteSpace(outputString))
            {
                output = JsonSerializer.Deserialize<TOutput>(
                    outputString,
                    _serializerOptions
                );
            }

            return output;
        }
        private string PrepareRoute(string route, object? queryStringParametersObject)
        {
            if (queryStringParametersObject is null)
                return route;

            var parameters = JsonSerializer.Serialize(
                 queryStringParametersObject,
                 _serializerOptions);
            var parametersDictionary = Newtonsoft.Json.JsonConvert
                .DeserializeObject<Dictionary<string, string>>(parameters);

            return QueryHelpers.AddQueryString(route, parametersDictionary!);

        }
    }
}
