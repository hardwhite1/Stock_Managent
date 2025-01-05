namespace MyShop.Services
{
    public class Pagination
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set;}
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        public int StartIndex => (CurrentPage-1) * PageSize;

        public Pagination(int totalItems, int currentPage, int pageSize)
        {
            TotalItems = totalItems;
            CurrentPage = currentPage < 1 ? 1 : currentPage;
            PageSize = pageSize;
        }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}