﻿using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using FC.CodeFlix.Catalog.Domain.Interfaces;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.List
{
    public class ListCategories : IListCategories
    {
        private readonly ICategoryRepository _categoryRepository;

        public ListCategories(ICategoryRepository categoryRepository)
        => _categoryRepository = categoryRepository;

        public async Task<ListCategoriesOutput> Handle(ListCategoriesInput request, CancellationToken cancellationToken)
        {
            var searchOutput = await _categoryRepository.Search(
                                                          new(request.Page,
                                                          request.PerPage,
                                                          request.Search,
                                                          request.Sort,
                                                          request.Dir
                                                          )
                                                          , cancellationToken);

            return new ListCategoriesOutput(
                searchOutput.CurrentPage,
                searchOutput.PerPage,
                searchOutput.Total,
                searchOutput.Items.Select(CategoryModelOuput.FromCategory).ToList()
                );
        }
    }
}
