using FC.CodeFlix.Catalog.Application.Common;

namespace FC.CodeFlix.Catalog.Api.Models.Responses
{
    public class ApiResponseList<IItemData> : ApiResponse<IReadOnlyList<IItemData>>
    {
        public ApiResponseMeta Meta { get; private set; }
        public ApiResponseList(int currentPage,int perPage,int total, IReadOnlyList<IItemData> data)
            : base(data)
        {
            Meta = new ApiResponseMeta(currentPage, perPage, total);
        }
        public ApiResponseList(PaginatedListOutput<IItemData> data)
            : base(data.Items)
        {
            Meta = new ApiResponseMeta(data.CurrentPage, data.PerPage, data.Total);
        }
    }
}
