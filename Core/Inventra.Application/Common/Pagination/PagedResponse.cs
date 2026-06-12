using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Common.Pagination
{
    public sealed class PagedResponse<T>
    {
        public IReadOnlyList<T> Items { get; init; }
            = [];

        public int PageNumber { get; init; }

        public int PageSize { get; init; }

        public int TotalCount { get; init; }

        public int TotalPages { get; init; }
    }
}
