using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.Mapper.CompanyListingMappers
{
    public static class CompanyListingMapper
    {
        #region UpdateCompanyMethod

        /// <summary>
        /// Update List on the bases of Source Object
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public static CompanyListings MapCompanyListing(CompanyListings source, CompanyListings dest)
        {
            dest.Id = source.Id;
            dest.CompanyName = source.CompanyName;
            dest.FirstName = source.FirstName;
            dest.LastName = source.LastName;
            dest.Email = source.Email==null? dest.Email : source.Email;
            dest.Website = source.Website;
            dest.MetaTitle = source.MetaTitle;
            dest.MetaKeyword = source.MetaKeyword;
            dest.MetaDescription = source.MetaDescription;
            dest.ListingStatus = source.ListingStatus;
            dest.CustomerId = source.CustomerId > 0 ? source.CustomerId : dest.CustomerId;
            dest.ListingTypeId = source.ListingTypeId;
            dest.UpdatedBy = source.UpdatedBy;
            dest.UpdatedDate = source.UpdatedDate;
            return dest;
        }

        #endregion

    }
}
