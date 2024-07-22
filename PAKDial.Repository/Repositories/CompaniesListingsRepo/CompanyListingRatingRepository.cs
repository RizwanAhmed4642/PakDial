using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using PAKDial.Repository.BaseRepository;
using PAKDial.StoreProcdures.AutoJobsProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAKDial.Repository.Repositories.CompaniesListingsRepo
{
    public class CompanyListingRatingRepository : BaseRepository<CompanyListingRating, decimal>, ICompanyListingRatingRepository
    {
        public CompanyListingRatingRepository(PAKDialSolutionsContext context):base(context)
        {
                
        }
        protected override DbSet<CompanyListingRating> DbSet
        {
            get
            {
                return db.CompanyListingRating;
            }
        }

        public AddListingRatingWrapperModel CompanyPostRating(CompanyListingRating rating)
        {
            decimal Result = 0;
            AddListingRatingWrapperModel addListing = new AddListingRatingWrapperModel();
            var phoneNumber = db.CompanyListingRating.Where(c => c.ListingId == rating.ListingId && c.MobileNo == rating.MobileNo).Count();
            if (phoneNumber < 1)
            {
                db.CompanyListingRating.Add(rating);
                Result = db.SaveChanges();
                if(Result > 0)
                {
                    addListing.Id = rating.Id;
                }
                else
                {
                    addListing.Message = "Record Faild";

                }
            }
            else
            {
                addListing.Message = "Number Already Exits";
            }
            return addListing;
        }

        public AddListingRatingWrapperModel PostRatingOtp(CompanyRatingsOTP ratingsOTP)
        {
            AddListingRatingWrapperModel addListing = new AddListingRatingWrapperModel();
            var results = db.CompanyListingRating.Where(c => c.Id == ratingsOTP.Id).FirstOrDefault();
            if(ratingsOTP.Delete == false)
            {
                results.IsApproved = true;
                results.IsVerified = true;
                db.CompanyListingRating.Update(results);
                if (db.SaveChanges() > 0)
                {
                    addListing.Id = 1;
                    addListing.Message = "Review Approved and Live Successfully";
                }
            }
            else
            {
                db.CompanyListingRating.Remove(results);
                if (db.SaveChanges() > 0)
                {
                    addListing.Id = 3;
                    addListing.Message = "Review Locked and Removed";
                }
            }
            return addListing;
        }

        public ListingRatingWrapperModel FindByListingIdFront(decimal ListingId)
        {
            ListingRatingWrapperModel response = new ListingRatingWrapperModel
            {
                ListingRating = db.CompanyListingRating.Where(c => c.ListingId == ListingId && c.IsVerified == true).OrderBy(c => c.Id).Take(10)
                                .Select(c => new ListingRatingModel { Id = c.Id, Name = c.Name, RatingDesc = c.RatingDesc, Rating = (int)c.Rating, CreatedDate = (DateTime)c.CreatedDate,ListingId = c.ListingId }).ToList(),
                IncrementalCount = 10,
                RowCount = db.CompanyListingRating.Where(c => c.ListingId == ListingId && c.IsVerified == true).Count()
            };
            return response;
        }

        public ListingRatingWrapperModel FindByListingIdFront(decimal ListingId,int IncrementalCount)
        {
            ListingRatingWrapperModel response = new ListingRatingWrapperModel
            {
                ListingRating = db.CompanyListingRating.Where(c => c.ListingId == ListingId && c.IsVerified == true).OrderBy(c => c.Id).Skip(IncrementalCount).Take(10)
                                 .Select(c => new ListingRatingModel { Id = c.Id, Name = c.Name, RatingDesc = c.RatingDesc, Rating = (int)c.Rating, CreatedDate = (DateTime)c.CreatedDate, ListingId = c.ListingId }).ToList(),
                IncrementalCount = IncrementalCount + 10,
                RowCount = db.CompanyListingRating.Where(c => c.ListingId == ListingId && c.IsVerified == true).Count()
            };
            return response;
        }

        public List<CompanyListingRating> FindByListingId(decimal ListingId)
        {
            return db.CompanyListingRating.Where(c => c.ListingId == ListingId).ToList(); 
        }

        public CompanyListingRating FindByListingId(decimal ListingId, string MobileNo)
        {
            return db.CompanyListingRating.Where(c => c.ListingId == ListingId && c.MobileNo == MobileNo).FirstOrDefault();
        }

        public List<CompanyListingRating> GetVerifiedRating(bool IsVerified)
        {
            return db.CompanyListingRating.Where(c => c.IsVerified == IsVerified).ToList();
        }

        public List<CompanyListingRating> GetVerifiedRating(bool IsVerified, decimal ListingId)
        {
            return db.CompanyListingRating.Where(c => c.ListingId == ListingId && c.IsVerified == IsVerified).ToList();
        }

        public void AutoDeleteUnVerfiedRating()
        {
            AutoJobsSp.AutoDeleteUnVerfiedRating();
        }
    }
}
