namespace FC.CodeFlix.Catalog.Api.Models.Responses
{
    public class ApiResponse<IData>
    {
        public IData Data { get; private set; }

        public ApiResponse(IData data)
        => Data = data;

    }
}
