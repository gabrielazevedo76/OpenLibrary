using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenLibrary.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.Data.Mappings
{
    public class RatingMapping : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.TotalRating)
            .IsRequired()
            .HasColumnType("smallint");

            // 1 : N => Rating : UserRatigns
            builder.HasMany(u => u.UserRatings)
            .WithOne(r => r.Rating)
            .HasForeignKey(r => r.RatingId);

            // 1 : 1 => Rating : Book
            builder.HasOne(u => u.Book)
            .WithOne(b => b.Rating);
         
            builder.ToTable("Rating");
        }
    }
}
