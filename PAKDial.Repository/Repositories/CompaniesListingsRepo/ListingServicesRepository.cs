using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAKDial.Repository.Repositories.CompaniesListingsRepo
{
    public class ListingServicesRepository : BaseRepository<ListingServices, decimal>, IListingServicesRepository
    {
        public ListingServicesRepository(PAKDialSolutionsContext context)
              : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<ListingServices> DbSet
        {
            get
            {
                return db.ListingServices;
            }
        }
        public List<ListingServices> GetByListingId(decimal ListingId)
        {
            return DbSet.Where(c => c.ListingId == ListingId).ToList();
        }

        public ListingServices GetListingByService(decimal ServiceTypeId, decimal ListingId)
        {
            return DbSet.Where(c => c.ServiceTypeId == ServiceTypeId && c.ListingId == ListingId).FirstOrDefault();
        }

        public List<ListingServices> GetListingByServiceId(decimal ServiceTypeId)
        {
            return DbSet.Where(c => c.ServiceTypeId == ServiceTypeId).ToList();
        }
    }
}
