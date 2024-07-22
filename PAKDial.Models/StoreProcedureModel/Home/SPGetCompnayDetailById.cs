using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.StoreProcedureModel.Home
{
   public class SPGetCompnayDetailById
    {
        #region Prop

        public  decimal ListingId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string BannerImage { get; set; }
        public string MobileNo { get; set; }
        public string ImageUrl { get; set; }
        public string Address{ get; set; }
        public int AvgRating { get; set; }
        public int TotalVotes { get; set; }
        public bool IsPremium { get; set; }
        public int IsTrusted { get; set; }

        #endregion

        #region Ctor
        public SPGetCompnayDetailById()
        {
            ListingId = 0;
            CompanyName = string.Empty;
            Email = string.Empty;
            Website = string.Empty;
            BannerImage = string.Empty;
            MobileNo = string.Empty;
            ImageUrl = string.Empty;
            Address = string.Empty;
            AvgRating = 0;
            TotalVotes = 0;
            IsPremium = false;
        }
        #endregion
    }
}
