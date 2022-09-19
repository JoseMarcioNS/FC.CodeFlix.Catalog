﻿namespace FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository
{
    public class SearchInput
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public string Search { get; set; }
        public string OrderBy { get; set; }
        public SearchOrder Oder { get; set; }
        public SearchInput(int page, int perPage, string search, string orderBy, SearchOrder oder)
        {
            Page = page;
            PerPage = perPage;
            Search = search;
            OrderBy = orderBy;
            Oder = oder;
        }
    }
}
