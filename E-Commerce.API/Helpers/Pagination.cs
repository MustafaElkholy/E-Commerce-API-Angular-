namespace E_Commerce.API.Helpers
{
    public class Pagination <T> where T : class
    {
        public Pagination(int page, int pageSize, int count, IEnumerable<T> data)
        {
            Page = page;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
