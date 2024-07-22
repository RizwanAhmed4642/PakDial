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
    public class CompanyListingProfileRepository : BaseRepository<CompanyListingProfile, decimal>, ICompanyListingProfileRepository
    {
        public CompanyListingProfileRepository(PAKDialSolutionsContext context)
           : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<CompanyListingProfile> DbSet
        {
            get
            {
                return db.CompanyListingProfile;
            }
        }

        public CompanyListingProfile GetByListingId(decimal ListingId)
        {
            return DbSet.Where(c => c.ListingId == ListingId).FirstOrDefault();
        }
    }
}
