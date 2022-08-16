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
    public class UserRatingMapping : IEntityTypeConfiguration<UserRating>
    {
        public void Configure(EntityTypeBuilder<UserRating> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Rate)
            .IsRequired()
            .HasColumnType("smallint");

            builder.Property(p => p.Comment)
            .HasColumnType("varchar(2000)");

            builder.ToTable("UserRating");
        }
    }
}
