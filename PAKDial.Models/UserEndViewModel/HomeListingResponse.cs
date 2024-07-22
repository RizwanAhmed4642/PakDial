using PAKDial.Domains.SqlViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.UserEndViewModel
{
    public class HomeListingResponse
    {
        public HomeListingResponse()
        {
            HomeListingSearch = new List<VHomeListingSearch>();
        }
        //CityName
        public string CtName { get; set; }
        //SubCategoryId
        public decimal CatId { get; set; }
        public decimal SbCId { get; set; }
        //SubCategoryName
        public string SbCName { get; set; }
        //AreaName
        public string ArName { get; set; }
        public string Ratingstatus { get; set; }
        public string SortColumnName { get; set; }

        public int PageNo { get; set; }
        public int RowCount { get; set; }
        public int PageSize { get; set; }
        public List<VHomeListingSearch> HomeListingSearch { get; set; }
    }

}
