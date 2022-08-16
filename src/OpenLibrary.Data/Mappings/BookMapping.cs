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
    public class BookMapping : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
            .IsRequired()
            .HasColumnType("varchar(200)");

            builder.Property(p => p.Sinopsis)
            .IsRequired()
            .HasColumnType("varchar(2000)");

            builder.Property(p => p.Imagem)
            .IsRequired()
            .HasColumnType("varchar(100)");

            builder.ToTable("Book");
        }
    }
}
