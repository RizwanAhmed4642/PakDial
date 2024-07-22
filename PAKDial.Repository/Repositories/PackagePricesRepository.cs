using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Interfaces.Repository;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PAKDial.Repository.Repositories
{
    public class PackagePricesRepository : BaseRepository<PackagePrices, decimal>, IPackagePricesRepository
    {
        public PackagePricesRepository(PAKDialSolutionsContext context)
         : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<PackagePrices> DbSet
        {
            get
            {
                return db.PackagePrices;
            }
        }

        public PackagePricesResponse GetPackagePrices(PackagePricesRequestModel request)
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
                Expression<Func<VPackagePrices, bool>> query =
                    exp =>
                        (exp.Id == request.Id);

                int rowCount = db.VPackagePrices.Count(query);
                // Server Side Pager
                IEnumerable<VPackagePrices> packagePrices = request.IsAsc
                    ? db.VPackagePrices.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList()
                    : db.VPackagePrices.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList();
                return new PackagePricesResponse
                {
                    RowCount = rowCount,
                    PackagePrices = packagePrices
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public List<VPackagePrices> GetAllPackagePrices(decimal Id)
        {
            return db.VPackagePrices.Where(c => c.Id == Id).OrderByDescending(c => c.PriceId).ToList();
        }

        public VPackagePrices GetPackagePriceById(decimal Id)
        {
            return db.VPackagePrices.Where(c => c.Id == Id).FirstOrDefault();
        }
    }
}
