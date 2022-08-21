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
    public class PublisherMapping : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
            .IsRequired()
            .HasColumnType("varchar(100)");

            builder.Property(p => p.Email)
            .IsRequired()
            .HasColumnType("varchar(100)");

            builder.Property(p => p.CNPJ)
            .IsRequired()
            .HasColumnType("varchar(14)");

            builder.HasMany(x => x.Books)
            .WithOne(x => x.Publisher)
            .HasForeignKey(x => x.PublisherId);

            builder.ToTable("Publisher");
        }
    }
}
