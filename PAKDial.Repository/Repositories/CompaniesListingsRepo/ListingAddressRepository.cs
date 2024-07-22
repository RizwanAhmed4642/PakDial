using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using PAKDial.Repository.BaseRepository;
using PAKDial.StoreProcdures.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAKDial.Repository.Repositories.CompaniesListingsRepo
{
    public class ListingAddressRepository : BaseRepository<ListingAddress, decimal>, IListingAddressRepository
    {
        public ListingAddressRepository(PAKDialSolutionsContext context)
            : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<ListingAddress> DbSet
        {
            get
            {
                return db.ListingAddress;
            }
        }

        public string GetAddressByListingId(decimal ListingId)
        {
            return UserFrontStoreProcedure.GetAddressByListingId(ListingId);
        }

        public List<ListingAddress> GetByListingId(decimal ListingId)
        {
            var Address = DbSet.Where(c => c.ListingId == ListingId).ToList();
            foreach (var item in Address)
            {
                item.CityAreaName = db.CityArea.Where(c => c.Id == item.CityAreaId).FirstOrDefault().Name;
            }
            return Address;
        }
    }
}
