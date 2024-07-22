using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class ListingGallery
    {
        public decimal Id { get; set; }
        public string FileName { get; set; }
        public string UploadDir { get; set; }
        public string FileType { get; set; }
        public string FileUrl { get; set; }
        public decimal ListingId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public CompanyListings Listing { get; set; }
    }
}
