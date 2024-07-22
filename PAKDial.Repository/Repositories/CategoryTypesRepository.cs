using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Interfaces.Repository;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Repository.Repositories
{
    public class CategoryTypesRepository : BaseRepository<CategoryTypes, decimal>, ICategoryTypesRepository
    {
        public CategoryTypesRepository(PAKDialSolutionsContext context)
            : base(context)
        {

        }

        /// Primary database set
        protected override DbSet<CategoryTypes> DbSet
        {
            get
            {
                return db.CategoryTypes;
            }
        }
    }
}
