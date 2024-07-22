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
    public class VerifiedListingRepository : BaseRepository<VerifiedListing, decimal>, IVerifiedListingRepository
    {
        public VerifiedListingRepository(PAKDialSolutionsContext context)
             : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<VerifiedListing> DbSet
        {
            get
            {
                return db.VerifiedListing;
            }
        }
        public List<VerifiedListing> GetByListingId(decimal ListingId)
        {
            return DbSet.Where(c => c.ListingId == ListingId).ToList();
        }

        public VerifiedListing GetListingByService(decimal VerificationId, decimal ListingId)
        {
            return DbSet.Where(c => c.VerificationId == VerificationId && c.ListingId == ListingId).FirstOrDefault();
        }

        public List<VerifiedListing> GetListingByServiceId(decimal VerificationId)
        {
            return DbSet.Where(c => c.VerificationId == VerificationId).ToList();
        }
    }
}
