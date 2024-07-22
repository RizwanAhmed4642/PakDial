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
    public class ListingMobileNoRepository : BaseRepository<ListingMobileNo, decimal>, IListingMobileNoRepository
    {
        public ListingMobileNoRepository(PAKDialSolutionsContext context)
            : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<ListingMobileNo> DbSet
        {
            get
            {
                return db.ListingMobileNo;
            }
        }
        public List<ListingMobileNo> GetByListingId(decimal ListingId )
        {
            return DbSet.Where(c => c.ListingId == ListingId).ToList();
        }

        public List<ListingMobileNo> GetListingMobileNoByName(string MobileNo,int Id)
        {
            if(Id>0)
            {
                return DbSet.Where(c => c.MobileNo == MobileNo && Id!=c.ListingId).ToList();
            }
            else
            {
                return DbSet.Where(c => c.MobileNo == MobileNo).ToList();
            }
           
        }
    }
}
