using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.ICompaniesListingsService
{
    public interface IListingCategoryService
    {
        ListingCategory FindById(decimal Id);
        IEnumerable<ListingCategory> GetAll();
        List<ListingCategory> GetByListingId(decimal ListingId);
        List<ListingCategory> GetListingCategories(decimal MainCategoryId);
        List<ListingCategory> GetListingCategories(decimal? SubCategoryId, string CategoryName);
        List<ListingCategory> GetListingCategories(decimal SubCategoryId, decimal MainCategoryId);
        ListingCategory GetListingCategories(decimal MainCategoryId, decimal SubCategoryId, decimal ListingId);
    }
}
