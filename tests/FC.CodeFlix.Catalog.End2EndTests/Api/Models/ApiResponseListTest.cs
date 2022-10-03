namespace FC.CodeFlix.Catalog.End2EndTests.Api.Models
{
    public class ApiResponseListTest<TOutputItem>
     : ApiResponseTest<List<TOutputItem>>
    {
        public ApiResponseListMetaTest? Meta { get; set; }

        public ApiResponseListTest(List<TOutputItem> data) : base(data) { }

        public ApiResponseListTest()
        { }

        public ApiResponseListTest(
            List<TOutputItem> data,
            ApiResponseListMetaTest meta
        ) : base(data)
            => Meta = meta;
    }

    public class ApiResponseListMetaTest
    {
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }

        public ApiResponseListMetaTest()
        { }

        public ApiResponseListMetaTest(int currentPage, int perPage, int total)
        {
            CurrentPage = currentPage;
            PerPage = perPage;
            Total = total;
        }
    }

    public class ApiResponseTest<TOutput>
    {
        public TOutput? Data { get; set; }

        public ApiResponseTest()
        { }

        public ApiResponseTest(TOutput data)
        {
            Data = data;
        }
    }
}
