namespace Petronas.SampleApp.Data.Model.Classes
{
    public class PageInfo
    {
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public string OrderBy { get; set; }
        public int UserId { get; set; }

        public PageInfo(int pageSize, int pageNo, string orderBy = null)
        {
            PageSize = pageSize;
            PageNo = pageNo;
            OrderBy = orderBy;
        }
    }
}