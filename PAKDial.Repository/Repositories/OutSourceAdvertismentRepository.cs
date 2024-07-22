using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Interfaces.Repository;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PAKDial.Repository.Repositories
{
    public class OutSourceAdvertismentRepository : BaseRepository<OutSourceAdvertisment, decimal>, IOutSourceAdvertismentRepository
    {
        public OutSourceAdvertismentRepository(PAKDialSolutionsContext context)
          : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<OutSourceAdvertisment> DbSet
        {
            get
            {
                return db.OutSourceAdvertisment;
            }
        }

        public void UploadImages(decimal Id, string ImagePath)
        {
            var AddsImages = DbSet.Where(c => c.Id == Id).FirstOrDefault();
            AddsImages.ImagePath = ImagePath;
            Update(AddsImages);
        }
        public void MobUploadImages(decimal Id, string MobImagePath)
        {
            var AddsImages = DbSet.Where(c => c.Id == Id).FirstOrDefault();
            AddsImages.MobImagePath = MobImagePath;
            Update(AddsImages);
        }
        public bool CheckExistance(OutSourceAdvertisment instance)
        {
            bool Results = false;
            if (instance.Id > 0)
            {
                Results = DbSet.Where(c => c.Name.ToLower().Trim() == instance.Name.ToLower().Trim() && c.Id != instance.Id)
                    .Count() > 0 ? true : false;
            }
            else
            {
                Results = DbSet.Where(c => c.Name.ToLower().Trim() == instance.Name.ToLower().Trim())
                    .Count() > 0 ? true : false;
            }
            return Results;
        }
        public List<VMOutSourceAdvertisment> GetLoadHomeSlider()
        {
            return DbSet.Where(c => c.IsActive == true).Select(c => new VMOutSourceAdvertisment { Id = c.Id, Name = c.Name, ImagePath = c.ImagePath, ImageUrl = c.ImageUrl, MobImagePath = c.MobImagePath }).ToList();
        }
        public OutSourceAdvertismentResponse GetAdvertismentList(OutSourceAdvertismentRequestModel request)
        {
            try
            {
                int fromRow = 0;
                if (request.PageNo == 1)
                {
                    fromRow = (request.PageNo - 1) * request.PageSize;
                }
                else
                {
                    fromRow = request.PageNo;
                }
                int toRow = request.PageSize;
                bool isSearchFilterSpecified = !string.IsNullOrEmpty(request.SearchString);
                Expression<Func<OutSourceAdvertisment, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.Name.Contains(request.SearchString))) ||
                         !isSearchFilterSpecified);

                int rowCount = DbSet.Count(query);
                // Server Side Pager
                IEnumerable<OutSourceAdvertisment> OutSourceAdvertisments = request.IsAsc
                    ? DbSet.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : DbSet.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();
                return new OutSourceAdvertismentResponse
                {
                    RowCount = rowCount,
                    outSourceAdvertisments = OutSourceAdvertisments
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
