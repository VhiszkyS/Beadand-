﻿// <auto-generated />
using System;
using Beadandó.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Beadandó.Migrations.Reader
{
    [DbContext(typeof(ReaderContext))]
    [Migration("20241130175016_ok")]
    partial class ok
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("BeadandóShared.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("ReaderId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ReaderId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("BeadandóShared.Reader", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Readers");
                });

            modelBuilder.Entity("BeadandóShared.Item", b =>
                {
                    b.HasOne("BeadandóShared.Reader", null)
                        .WithMany("Items")
                        .HasForeignKey("ReaderId");
                });

            modelBuilder.Entity("BeadandóShared.Reader", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
