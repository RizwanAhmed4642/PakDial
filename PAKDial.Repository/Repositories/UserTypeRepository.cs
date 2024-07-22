using Microsoft.EntityFrameworkCore;
using PAKDial.Interfaces.Repository;
using PAKDial.Domains.DomainModels;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Repository.Repositories
{

    public class UserTypeRepository : BaseRepository<UserType, decimal>, IUserTypeRepository
    {
        public UserTypeRepository(PAKDialSolutionsContext context)
            : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<UserType> DbSet
        {
            get
            {
                return db.UserType;
            }
        }

    }
}
