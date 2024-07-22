using PAKDial.Domains.DomainModels;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.CompaniesListingsService
{
    public class CompanyListingRatingService : ICompanyListingRatingService
    {
        private readonly ICompanyListingRatingRepository companyListingRatingRepository;
        public CompanyListingRatingService(ICompanyListingRatingRepository companyListingRatingRepository)
        {
            this.companyListingRatingRepository = companyListingRatingRepository;

        }

        public AddListingRatingWrapperModel CompanyPostRating(CompanyListingRating rating)
        {
            if (rating == null)
            {
                throw new ArgumentNullException("instance");
            }
            return companyListingRatingRepository.CompanyPostRating(rating);
        }
        public AddListingRatingWrapperModel PostRatingOtp(CompanyRatingsOTP ratingsOTP)
        {
            return companyListingRatingRepository.PostRatingOtp(ratingsOTP);
        }
        public decimal Add(CompanyListingRating instance)
        {
            var Saved = 0;//Rating not Saved
            if (instance == null)
            {
                throw new  ArgumentNullException("instance");
            }

            var CheckExistance = companyListingRatingRepository.FindByListingId(instance.ListingId,Convert.ToString(instance.MobileNo));
            if (CheckExistance ==  null)
            {
                instance.OptCode = 1245;  //Need Function For Four Digit Random Number Generator..
                companyListingRatingRepository.Add(instance);
                Saved = companyListingRatingRepository.SaveChanges();
            }
            return Saved;
        }

        public decimal Update(CompanyListingRating instance)
        {
            var Saved = 0;// rating not deleted
            var CheckRow = companyListingRatingRepository.Find(instance.Id);
            if (CheckRow != null)
            {
                if (CheckRow.OptCode == instance.OptCode)
                {
                    CheckRow.IsApproved = true;
                    CheckRow.IsVerified = true;
                    companyListingRatingRepository.Update(CheckRow);
                    Saved = companyListingRatingRepository.SaveChanges();
                    //After Save Changes We Will Send Opt Code on Email and Mobile Phone
                }
                else
                {
                    if(CheckRow.TotalAttempts < 3)
                    {
                        CheckRow.TotalAttempts = CheckRow.TotalAttempts + 1;
                        companyListingRatingRepository.Update(CheckRow);
                        if(companyListingRatingRepository.SaveChanges() > 0)
                        {
                            Saved = 2; //Opt code is not matched
                        }
                    }
                    else
                    {
                        Saved = 3; // No More Attempt                    
                    }
                }
            }
            return Saved;
        }

        public decimal Delete(decimal Id)
        {
            var Saved = 0;// rating not deleted
            var CheckRow = companyListingRatingRepository.Find(Id);
            if (CheckRow != null)
            {
                companyListingRatingRepository.Delete(CheckRow);
                Saved =  companyListingRatingRepository.SaveChanges(); // 1 Record Saved Successfully
            }
            return Saved;
        }
        
        public List<CompanyListingRating> FindByListingId(decimal ListingId)
        {
            return companyListingRatingRepository.FindByListingId(ListingId);
        }

        public CompanyListingRating FindByListingId(decimal ListingId, string MobileNo)
        {
            return companyListingRatingRepository.FindByListingId(ListingId,MobileNo);
        }

        public List<CompanyListingRating> GetVerifiedRating(bool IsVerified)
        {
            return companyListingRatingRepository.GetVerifiedRating(IsVerified);
        }

        public List<CompanyListingRating> GetVerifiedRating(bool IsVerified, decimal ListingId)
        {
            return companyListingRatingRepository.GetVerifiedRating(IsVerified, ListingId);
        }

        public ListingRatingWrapperModel FindByListingIdFront(decimal ListingId)
        {
            return companyListingRatingRepository.FindByListingIdFront(ListingId);
        }
        public ListingRatingWrapperModel FindByListingIdFront(decimal ListingId, int IncrementalCount)
        {
            return companyListingRatingRepository.FindByListingIdFront(ListingId, IncrementalCount);
        }

        public void AutoDeleteUnVerfiedRating()
        {
            companyListingRatingRepository.AutoDeleteUnVerfiedRating();
        }
    }
}
