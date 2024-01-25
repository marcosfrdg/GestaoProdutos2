using Application.Core.Abstractions.Messages;
using System.Collections.Generic;

namespace Application.Products.Queries
{
    public class GetProductListQuery : IQuery<List<ProductResponse>>
    {
        public GetProductListQuery(int page, int pageSize, string filter)
        {
            Page = page;
            PageSize = pageSize;
            Filter = filter;
        }

        public int Page { get; }
        public int PageSize { get; }
        public string Filter { get; }
    }
}
