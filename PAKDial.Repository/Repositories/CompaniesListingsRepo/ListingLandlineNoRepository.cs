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
    public class ListingLandlineNoRepository : BaseRepository<ListingLandlineNo, decimal>, IListingLandlineNoRepository
    {
        public ListingLandlineNoRepository(PAKDialSolutionsContext context)
           : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<ListingLandlineNo> DbSet
        {
            get
            {
                return db.ListingLandlineNo;
            }
        }
        public List<ListingLandlineNo> GetByListingId(decimal ListingId)
        {
            return DbSet.Where(c => c.ListingId == ListingId).ToList();
        }

        public List<ListingLandlineNo> GetListingLandlineNoByName(string LandLineNo ,int Id)
        {
            if (Id>0)
            {
                return DbSet.Where(c => c.LandlineNumber == LandLineNo && Id!=c.ListingId).ToList();
            }
            else
            {
                return DbSet.Where(c => c.LandlineNumber == LandLineNo).ToList();
            }
            
        }
    }
}
