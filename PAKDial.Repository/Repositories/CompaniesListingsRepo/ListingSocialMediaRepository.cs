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
    public class ListingSocialMediaRepository : BaseRepository<ListingSocialMedia, decimal>, IListingSocialMediaRepository
    {
        public ListingSocialMediaRepository(PAKDialSolutionsContext context)
           : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<ListingSocialMedia> DbSet
        {
            get
            {
                return db.ListingSocialMedia;
            }
        }
        public List<ListingSocialMedia> GetByListingId(decimal ListingId)
        {
            return DbSet.Where(c => c.ListingId == ListingId).OrderBy(c=>c.MediaId).ToList();

        }

        public ListingSocialMedia GetListingSocialMedia(decimal MediaId, decimal ListingId)
        {
            return DbSet.Where(c => c.MediaId == MediaId && c.ListingId == ListingId).FirstOrDefault();
        }

        public List<ListingSocialMedia> GetListingSocialMediaId(decimal MediaId)
        {
            return DbSet.Where(c => c.MediaId == MediaId).ToList();
        }

        public SocialMediaModes GetSocialMediaModes(decimal MediaId)
        {
             return db.SocialMediaModes.Where(c => c.Id == MediaId).FirstOrDefault();
        }

        public List<SocialMediaModes> SocialMediaModesList()
        {
            return db.SocialMediaModes.ToList();
        }
    }
}
