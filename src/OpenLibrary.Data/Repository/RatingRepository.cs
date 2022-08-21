using Microsoft.EntityFrameworkCore;
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
    public class RatingRepository : Repository<Rating>, IRatingRepository
    {
        public RatingRepository(OpenLibraryDbContext context) : base(context) {}

        public async Task<Rating> GetByIdWithRelation(Guid id)
        {
            var RatingWithBook = await Db.Ratings
                .Include(x => x.Book)
                .AsNoTracking()
                .Include(x => x.UserRatings)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return RatingWithBook;
        }
    }
}
