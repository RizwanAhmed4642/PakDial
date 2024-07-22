using PAKDial.Domains.DomainModels;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.CompaniesListingsService
{
    public class ListingCategoryService : IListingCategoryService
    {
        private readonly IListingCategoryRepository listingCategoryRepository;

        public ListingCategoryService(IListingCategoryRepository listingCategoryRepository)
        {
            this.listingCategoryRepository = listingCategoryRepository;
        }

        public ListingCategory FindById(decimal Id)
        {
            return listingCategoryRepository.Find(Id);
        }

        public IEnumerable<ListingCategory> GetAll()
        {
            return listingCategoryRepository.GetAll();
        }

        public List<ListingCategory> GetByListingId(decimal ListingId)
        {
            return listingCategoryRepository.GetByListingId(ListingId);
        }

        public List<ListingCategory> GetListingCategories(decimal MainCategoryId)
        {
            return listingCategoryRepository.GetListingCategories(MainCategoryId);
        }

        public List<ListingCategory> GetListingCategories(decimal? SubCategoryId, string CategoryName)
        {
            return listingCategoryRepository.GetListingCategories(SubCategoryId,CategoryName);
        }

        public List<ListingCategory> GetListingCategories(decimal SubCategoryId, decimal MainCategoryId)
        {
            return listingCategoryRepository.GetListingCategories(SubCategoryId, MainCategoryId);
        }

        public ListingCategory GetListingCategories(decimal MainCategoryId, decimal SubCategoryId, decimal ListingId)
        {
            return listingCategoryRepository.GetListingCategories(MainCategoryId, SubCategoryId, ListingId);
        }
    }
}
