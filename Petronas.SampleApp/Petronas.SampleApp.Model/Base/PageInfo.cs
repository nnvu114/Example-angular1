using Petronas.SampleApp.Common;
using System.Collections.Generic;

namespace Petronas.SampleApp.Model.Classes
{
    public class PageInfo
    {
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public string OrderBy { get; set; }
        public EnumCommon.SortDirection Direction { get; set; }
        public List<string> Fields { get; set; }
        public List<string> Fieldvalues { get; set; }

        public PageInfo(int pageSize=10, int pageNo=1, string orderBy = null, EnumCommon.SortDirection direction=0)
        {
            PageSize = pageSize;
            PageNo = pageNo;
            OrderBy = orderBy;
            Direction = direction;
            Fields = new List<string>();
            Fieldvalues = new List<string>();
        }
        public PageInfo()
        {
            PageSize = 10;
            PageNo = 1;
            OrderBy = "";
            Direction = 0;
            Fields = new List<string>();
            Fieldvalues = new List<string>();
        }
    }
}