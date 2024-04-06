namespace ProductManagementMVCProject.Dto
{
    public class PageModel
    {
        public PageModel() { }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int StartRecord { get; set; }
        public int EndRecord { get; set; }

        public PageModel(int totalItems,int currentPage, int pageSize = 5)
        {
            this.TotalItems = totalItems;
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;

            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            TotalPage = totalPages;

            int startPage = currentPage - 5;
            int endPage = currentPage + 4;

            if(startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }

            if(endPage > totalPages)
            {
                endPage = totalPages;
                if(endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            StartRecord = (currentPage - 1) * PageSize + 1;

            if(EndRecord > TotalItems)
                EndRecord = TotalItems;

            if(TotalItems == 0)
            {
                StartPage = 0;
                StartRecord = 0;
                CurrentPage = 0;
                EndRecord = 0;
            }
            else
            {
                StartPage = startPage;
                EndPage = endPage;
            }
        }
    }
}
