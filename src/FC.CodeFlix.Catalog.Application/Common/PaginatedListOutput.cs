namespace FC.CodeFlix.Catalog.Application.Common
{
    public class PaginatedListOutput<TOutputItem>
    {
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public IReadOnlyList<TOutputItem> Items { get; set; }
        public PaginatedListOutput(int currentPage, int perPage, int total, IReadOnlyList<TOutputItem> items)
        {
            CurrentPage = currentPage;
            PerPage = perPage;
            Total = total;
            Items = items;
        }
    }
}
