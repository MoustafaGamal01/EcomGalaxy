namespace EcomGalaxy.ViewModel
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; init; } = Enumerable.Empty<T>();
        public int CurrentPage { get; init; }
        public int TotalPages { get; init; }
        public int TotalCount { get; init; }
        public int PageSize { get; init; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public IEnumerable<int> PageWindow(int windowSize = 5)
        {
            int half = windowSize / 2;
            int start = Math.Max(1, CurrentPage - half);
            int end = Math.Min(TotalPages, start + windowSize - 1);

            start = Math.Max(1, end - windowSize + 1);

            return Enumerable.Range(start, end - start + 1);
        }
    }
}