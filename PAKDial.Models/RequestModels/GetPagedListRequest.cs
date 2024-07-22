using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.RequestModels
{
    public class GetPagedListRequest
    {
        public GetPagedListRequest()
        {
            IsAsc = false;
            SortBy = 1;
            PageNo = 1;
            PageSize = 5;
        }

        public int PageSize { get; set; }

        public string SearchString { get; set; }
        public string SortColumnName { get; set; }

        private int _pageNo;

        public int PageNo
        {
            get
            {
                return _pageNo;
            }
            set
            {
                _pageNo = value == 0 ? 1 : value;
            }
        }

        public bool IsAsc { get; set; }

        public int Id { get; set; }

        public short SortBy { get; set; }

        public int TotalCount { get; set; }
    }
}
