using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.Repository;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAKDial.Repository.Repositories
{
    public class ListingsBusinessTypesRepository : BaseRepository<ListingsBusinessTypes, decimal>, IListingsBusinessTypesRepository
    {
        public ListingsBusinessTypesRepository(PAKDialSolutionsContext context)
           : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<ListingsBusinessTypes> DbSet
        {
            get
            {
                return db.ListingsBusinessTypes;
            }
        }

        public bool BusinessTypeExistance(decimal BusinessId)
        {
            return DbSet.Where(c => c.BusinessId == BusinessId).Count() > 0 ? true : false;
        }

        public bool CheckExistance(ListingsBusinessTypes instance)
        {
            bool Results = false;
            if (instance.Id > 0)
            {
                Results = DbSet.Where(c => c.ListingId == instance.ListingId && c.BusinessId == instance.BusinessId && c.Id != instance.Id)
                    .Count() > 0 ? true : false;
            }
            else
            {
                Results = DbSet.Where(c => c.ListingId == instance.ListingId && c.BusinessId == instance.BusinessId)
                    .Count() > 0 ? true : false;
            }
            return Results;
        }

        public List<ListingsBusinessTypes> GetByListingsId(decimal Id)
        {
            return DbSet.Where(c => c.ListingId == Id).OrderBy(c => c.BusinessId).ToList();

        }
    }
}
