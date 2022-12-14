using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BonifiQ.Domain.Entities
{
    public partial class BonifiQContext : DbContext
    {
        public BonifiQContext()
        {
        }

        public BonifiQContext(DbContextOptions<BonifiQContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Photo> Photos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("PHOTO");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AlbumId).HasColumnName("albumId");

                entity.Property(e => e.DateIncluded)
                    .HasColumnType("datetime")
                    .HasColumnName("dateIncluded");

                entity.Property(e => e.ThumbnailUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("thumbnailUrl");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.Url)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("url");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
