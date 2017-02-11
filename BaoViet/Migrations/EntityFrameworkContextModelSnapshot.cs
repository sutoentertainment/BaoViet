using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BaoViet.Services.EntityFramework;
using BaoVietCore.Models;

namespace BaoViet.Migrations
{
    [DbContext(typeof(EntityFrameworkContext))]
    partial class EntityFrameworkContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("BaoVietCore.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("OwnerId");

                    b.Property<string>("Source");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("BaoVietCore.Models.FeedItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date_Created");

                    b.Property<string>("Description");

                    b.Property<string>("Link");

                    b.Property<string>("Thumbnail");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Feeds");
                });

            modelBuilder.Entity("BaoVietCore.Models.PaperBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("CellWidth");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("HomePage");

                    b.Property<string>("ImageSource");

                    b.Property<int>("Index");

                    b.Property<string>("Title");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("PaperBase");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PaperBase");
                });

            modelBuilder.Entity("BaoVietCore.Models.CustomPaper", b =>
                {
                    b.HasBaseType("BaoVietCore.Models.PaperBase");

                    b.Property<DateTime>("Date_Created");

                    b.ToTable("CustomPaper");

                    b.HasDiscriminator().HasValue("CustomPaper");
                });

            modelBuilder.Entity("BaoVietCore.Models.Category", b =>
                {
                    b.HasOne("BaoVietCore.Models.PaperBase", "Owner")
                        .WithMany("Categories")
                        .HasForeignKey("OwnerId");
                });
        }
    }
}
