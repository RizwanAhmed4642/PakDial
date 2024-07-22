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
    public class CompanyListingTimmingRepository : BaseRepository<CompanyListingTimming, decimal>, ICompanyListingTimmingRepository
    {
        public CompanyListingTimmingRepository(PAKDialSolutionsContext context)
            : base(context)
        {

        }

        /// Primary database set
        protected override DbSet<CompanyListingTimming> DbSet
        {
            get
            {
                return db.CompanyListingTimming;
            }
        }

        public PaymentModes GetPaymentModes(decimal ModId)
        {
            return db.PaymentModes.Where(c=>c.Id==ModId).FirstOrDefault();
        }

        public List<CompanyListingTimming> GetTimmingByListingId(decimal ListingId)
        {
            return DbSet.Where(c => c.ListingId == ListingId).OrderBy(c=>c.WeekDayNo).AsNoTracking().ToList();
        }
    }
}
