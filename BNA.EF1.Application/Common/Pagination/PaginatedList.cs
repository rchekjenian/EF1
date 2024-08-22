namespace BNA.EF1.Application.Common.Pagination
{
    public sealed class PaginatedList<T>
    {
        public PaginatedList(int totalCount, int pageSize, int pageIndex, IEnumerable<T>? items)
        {
            TotalCount = totalCount;
            this.pageSize = pageSize;
            PageIndex = pageIndex;
            Items = items;
        }

        private int pageSize;

        public IEnumerable<T>? Items { get; }
        public int PageIndex { get; }
        public int PageSize { get => pageSize < TotalCount ? pageSize : TotalCount; }
        public int TotalCount { get; }
        public int TotalPages
        {
            get
            {
                double value = (TotalCount / (double)PageSize);

                return (int)Math.Ceiling(value);
            }
        }
    }
}
