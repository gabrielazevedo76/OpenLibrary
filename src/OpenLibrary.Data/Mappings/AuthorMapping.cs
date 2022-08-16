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
    public class AuthorMapping : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
            .IsRequired()
            .HasColumnType("varchar(100)");

            builder.Property(p => p.Description)
            .IsRequired()
            .HasColumnType("varchar(2000)");

            builder.HasMany(a => a.Books)
            .WithOne(p => p.Author)
            .HasForeignKey(p => p.AuthorId);
        }
    }
}
