using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PAKDial.Repository.Repositories.CompaniesListingsRepo
{
    public class ListingGalleryRepository : BaseRepository<ListingGallery, decimal>, IListingGalleryRepository
    {
        public ListingGalleryRepository(PAKDialSolutionsContext context)
           : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<ListingGallery> DbSet
        {
            get
            {
                return db.ListingGallery;
            }
        }
        public List<ListingGallery> GetByListingId(decimal ListingId)
        {
            return DbSet.Where(c => c.ListingId == ListingId).ToList();
        }

        public List<ListingGallery> GetSelectedVluByListingId(decimal ListingId)
        {
            return DbSet.Where(c => c.ListingId == ListingId).Select(c => new ListingGallery
            {
                Id = c.Id,
                UploadDir = c.UploadDir,
                ListingId = c.ListingId,
            }).ToList(); 
        }
        
    public decimal AddGalleryImages(List<ListingGallery> Entity)
        {
            decimal Result = 0;
            using (var Transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.ListingGallery.AddRange(Entity);
                    db.SaveChanges();
                    Transaction.Commit();
                    Result = 1;

                }
                catch (Exception ex)
                {
                    Transaction.Rollback();
                    return Result;
                }
                return Result;
            }
        }

        public decimal DeleteGalleryImages(string Url ,decimal Id, string path)
        {
            decimal result = 0;
            try
            {
               

                ListingGallery lObjListingGallery = db.ListingGallery.Where(x => x.Id == Id && x.UploadDir.Trim().ToLower()==Url.Trim().ToLower()).FirstOrDefault();
                if (File.Exists(path.Replace("\\", "/") + lObjListingGallery.UploadDir))
                {
                    var ImgPath = path.Replace("\\", "/") + lObjListingGallery.UploadDir;
                    File.Delete(ImgPath);
                }
                lObjListingGallery.UploadDir = null;
                lObjListingGallery.FileUrl = null;

                db.Entry(lObjListingGallery).State = EntityState.Deleted;
                db.SaveChanges();
                result = lObjListingGallery.Id;
            }
            catch (Exception ex)
            {
                return result;

            }

            return result;
        }

        public decimal UpdateGalleryImages(List<ListingGallery> Entity, decimal Id)
        {
            decimal Result = 0;
            using (var Transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var GalleryItem = db.ListingGallery.Where(c => c.ListingId == Id);
                    if (GalleryItem.Count() > 0)
                    {
                        db.ListingGallery.RemoveRange(GalleryItem);
                        db.SaveChanges();
                    }

                    db.ListingGallery.AddRange(Entity);
                    db.SaveChanges();
                    Transaction.Commit();
                    Result = 1;

                }
                catch (Exception ex)
                {
                    Transaction.Rollback();
                    return Result;
                }
                return Result;
            }
        }

       
    }
}
