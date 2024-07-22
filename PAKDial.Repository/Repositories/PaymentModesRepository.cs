using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.Repository;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PAKDial.Repository.Repositories
{
    public class PaymentModesRepository : BaseRepository<PaymentModes, Decimal>, IPaymentModesRepository
    {
        public PaymentModesRepository(PAKDialSolutionsContext context)
            : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<PaymentModes> DbSet
        {
            get
            {
                return db.PaymentModes;
            }
        }

        public override PaymentModes Find(decimal id)
        {
            return DbSet.Where(c=>c.Id == id).AsNoTracking().FirstOrDefault();
        }
        public bool CheckExistance(PaymentModes instance)
        {
            bool Results = false;

            if(instance.Id > 0)
            {
                Results = DbSet.Where(c => c.Name.Trim().ToLower() == instance.Name.Trim().ToLower() && c.Id != instance.Id).Count() > 0 ? true : false;
            }
            else
            {
                Results = DbSet.Where(c => c.Name.Trim().ToLower() == instance.Name.Trim().ToLower()).Count() > 0 ? true : false;
            }
            return Results;
        }

        public PaymentModesResponse GetPaymentModes(PaymentModesRequestModel request)
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
                Expression<Func<PaymentModes, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.Name.Contains(request.SearchString))) ||
                         !isSearchFilterSpecified);

                int rowCount = DbSet.Count(query);
                // Server Side Pager
                IEnumerable<PaymentModes> paymentMode = request.IsAsc
                    ? DbSet.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList()
                    : DbSet.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList();
                return new PaymentModesResponse
                {
                    RowCount = rowCount,
                    paymentModes = paymentMode
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public GetPaymentModesResponse GetPaymentModesList(PaymentModesRequestModel request)
        {
            try
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;

                Expression<Func<PaymentModes, bool>> query = exp =>
                       (string.IsNullOrEmpty(request.SearchString) || (exp.Name.Contains(request.SearchString.Trim().ToLower()))
                          && exp.IsActive == true);

                int rowCount = DbSet.Count(query);
                // Server Side Pager
                IEnumerable<VMGenericKeyValuePair<decimal>> paymentmode =
                      DbSet.Where(query)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList().Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.Name });

                return new GetPaymentModesResponse
                {
                    PageNo = fromRow,
                    RowCount = rowCount,
                    paymentModes = paymentmode
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public void UploadImages(decimal Id, string ImagePath, string AbsolutePath)
        {
            var Paymentsmodes = DbSet.Where(c => c.Id == Id).FirstOrDefault();
            Paymentsmodes.ImageDir = ImagePath;
            Paymentsmodes.ImageUrl = AbsolutePath;
            Update(Paymentsmodes);
        }
    }
}
