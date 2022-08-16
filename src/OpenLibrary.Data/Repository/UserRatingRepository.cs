using OpenLibrary.Business.Interfaces.Repository;
using OpenLibrary.Business.Models;
using OpenLibrary.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.Data.Repository
{
    public class UserRatingRepository : Repository<UserRating>, IUserRatingRepository
    {
        public UserRatingRepository(OpenLibraryDbContext context) : base(context)
        {
        }
    }
}
