using System.Collections.Generic;
using System.Linq;

namespace Domain.Primitives
{
    /// <summary>
    /// Represents the generic paged list.
    /// </summary>
    /// <typeparam name="T">The type of list.</typeparam>
    public sealed class PagedList<T>
    {
        public PagedList(IEnumerable<T> items, int page, int pageSize, int totalCount)
        {
            PageNumber = page;
            PageSize = pageSize;
            TotalItemCount = totalCount;
            Items = items.ToList();
        }

        /// <summary>
        /// Gets the current page.
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        /// Gets the page size. The maximum page size is 100.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Gets the total number of items.
        /// </summary>
        public int TotalItemCount { get; }

        /// <summary>
        /// Gets the flag indicating whether the next page exists.
        /// </summary>
        public bool HasNextPage => PageNumber * PageSize < TotalItemCount;

        /// <summary>
        /// Gets the flag indicating whether the previous page exists.
        /// </summary>
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// Gets the items.
        /// </summary>
        public IReadOnlyCollection<T> Items { get; }
    }
}
