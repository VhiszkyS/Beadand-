﻿// <auto-generated />
using System;
using Beadandó.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Beadandó.Migrations.Loan
{
    [DbContext(typeof(LoanContext))]
    partial class LoanContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("BeadandóShared.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("LoanId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LoanId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("BeadandóShared.Loan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BookId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LoanDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ReaderId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("BeadandóShared.Item", b =>
                {
                    b.HasOne("BeadandóShared.Loan", null)
                        .WithMany("Items")
                        .HasForeignKey("LoanId");
                });

            modelBuilder.Entity("BeadandóShared.Loan", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
