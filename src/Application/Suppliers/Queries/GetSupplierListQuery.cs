using Application.Core.Abstractions.Messages;
using System.Collections.Generic;

namespace Application.Suppliers.Queries
{
    public class GetSupplierListQuery : IQuery<List<SupplierResponse>>
    {
        public GetSupplierListQuery(int page, int pageSize, string filter)
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