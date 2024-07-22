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
    public class ListingPaymentsModeRepository: BaseRepository<ListingPaymentsMode, decimal>, IListingPaymentsModeRepository
    {
        public ListingPaymentsModeRepository(PAKDialSolutionsContext context)
           : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<ListingPaymentsMode> DbSet
        {
            get
            {
                return db.ListingPaymentsMode;
            }
        }

        public List<ListingPaymentsMode> GetByListingId(decimal ListingId)
        {
            return DbSet.Where(c => c.ListingId == ListingId).ToList();
        }

        public ListingPaymentsMode GetListingPaymentMode(decimal ModeId, decimal ListingId)
        {
            return DbSet.Where(c => c.ModeId == ModeId && c.ListingId == ListingId).FirstOrDefault();
        }

        public List<ListingPaymentsMode> GetListingPaymentModeId(decimal ModeId)
        {
            return DbSet.Where(c => c.ModeId == ModeId).ToList();
        }
    }
}
