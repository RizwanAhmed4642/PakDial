using PAKDial.Domains.RequestModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.UserEndViewModel
{
    public class HomeListingRequest:GetPagedListRequest
    {
        //CityName
        public string CtName { get; set; }
        //SubCategoryId
        public decimal SbCId { get; set; }
        //SubCategoryName
        public string SbCName { get; set; }
        //AreaName
        public string ArName{ get; set; }

        public string Ratingstatus { get; set; }

    }
}
