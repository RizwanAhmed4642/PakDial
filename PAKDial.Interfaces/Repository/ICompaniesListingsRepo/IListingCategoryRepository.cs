using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.ICompaniesListingsRepo
{
    public interface IListingCategoryRepository : IBaseRepository<ListingCategory,decimal>
    {
        List<ListingCategory> GetByListingId(decimal ListingId);
        List<ListingCategory> GetListingCategories(decimal MainCategoryId);
        List<ListingCategory> GetListingCategories(decimal? SubCategoryId, string CategoryName);
        List<ListingCategory> GetListingCategories(decimal SubCategoryId, decimal MainCategoryId);
        ListingCategory GetListingCategories(decimal MainCategoryId, decimal SubCategoryId, decimal ListingId);
    }
}
