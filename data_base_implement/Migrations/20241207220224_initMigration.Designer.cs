﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using data_base_implement;

#nullable disable

namespace data_base_implement.Migrations
{
    [DbContext(typeof(data_base))]
    [Migration("20241207220224_initMigration")]
    partial class initMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("contracts.storage_contracts.db_models.Department", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("FacultyId");

                    b.ToTable("departments");
                });

            modelBuilder.Entity("contracts.storage_contracts.db_models.Direction", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("alt_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("full_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("directions");
                });

            modelBuilder.Entity("contracts.storage_contracts.db_models.Document", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("TemplateId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("document_type")
                        .HasColumnType("int");

                    b.Property<int>("file_format_type")
                        .HasColumnType("int");

                    b.Property<string>("file_path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("TemplateId");

                    b.HasIndex("UserId");

                    b.ToTable("documets");
                });

            modelBuilder.Entity("contracts.storage_contracts.db_models.Faculty", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("faculties");
                });

            modelBuilder.Entity("contracts.storage_contracts.db_models.Student", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("StudentGroupId")
                        .HasColumnType("int");

                    b.Property<string>("fio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("grade_book_num")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("StudentGroupId");

                    b.ToTable("students");
                });

            modelBuilder.Entity("contracts.storage_contracts.db_models.StudentGroup", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("DirectionId")
                        .HasColumnType("int");

                    b.Property<int>("course_num")
                        .HasColumnType("int");

                    b.Property<int>("group_num")
                        .HasColumnType("int");

                    b.Property<int>("semester_num")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("DirectionId");

                    b.ToTable("student_groups");
                });

            modelBuilder.Entity("contracts.storage_contracts.db_models.StudentGroupUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("StudentGroupId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentGroupId");

                    b.HasIndex("UserId");

                    b.ToTable("studentGroupUsers");
                });

            modelBuilder.Entity("contracts.storage_contracts.db_models.Template", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("file_path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("templates");
                });

            modelBuilder.Entity("contracts.storage_contracts.db_models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<int>("academic_degree")
                        .HasColumnType("int");

                    b.Property<int>("academic_title")
                        .HasColumnType("int");

                    b.Property<string>("fio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("position")
                        .HasColumnType("int");

                    b.Property<DateOnly>("year_of_award_ad")
                        .HasColumnType("date");

                    b.Property<DateOnly>("year_of_award_at")
                        .HasColumnType("date");

                    b.Property<DateOnly>("year_of_birth")
                        .HasColumnType("date");

                    b.HasKey("id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("contracts.storage_contracts.db_models.Department", b =>
                {
                    b.HasOne("contracts.storage_contracts.db_models.Faculty", "faculty")
                        .WithMany("departments")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("faculty");
                });

            modelBuilder.Entity("contracts.storage_contracts.db_models.Direction", b =>
                {
                    b.HasOne("contracts.storage_contracts.db_models.Department", "department")
                        .WithMany("directions")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("department");
                });

            modelBuilder.Entity("contracts.storage_contracts.db_models.Document", b =>
                {
                    b.HasOne("contracts.storage_contracts.db_models.Template", "template")
                        .WithMany("documents")
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("contracts.storage_contracts.db_models.User", "user")
                        .WithMany("documents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("template");

                    b.Navigation("user");
                });

            modelBuilder.Entity("contracts.storage_contracts.db_models.Student", b =>
                {
                    b.HasOne("contracts.storage_contracts.db_models.StudentGroup", "student_group")
                        .WithMany("students")
                        .HasForeignKey("StudentGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("student_group");
                });

            modelBuilder.Entity("contracts.storage_contracts.db_models.StudentGroup", b =>
                {
                    b.HasOne("contracts.storage_contracts.db_models.Direction", "direction")
                        .WithMany("student_groups")
                        .HasForeignKey("DirectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("direction");
                });

            modelBuilder.Entity("contracts.storage_contracts.db_models.StudentGroupUser", b =>
                {
                    b.HasOne("contracts.storage_contracts.db_models.StudentGroup", null)
                        .WithMany()
                        .HasForeignKey("StudentGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("contracts.storage_contracts.db_models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("contracts.storage_contracts.db_models.User", b =>
                {
                    b.HasOne("contracts.storage_contracts.db_models.Department", "department")
                        .WithMany("users")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("department");
                });

            modelBuilder.Entity("contracts.storage_contracts.db_models.Department", b =>
                {
                    b.Navigation("directions");

                    b.Navigation("users");
                });

            modelBuilder.Entity("contracts.storage_contracts.db_models.Direction", b =>
                {
                    b.Navigation("student_groups");
                });

            modelBuilder.Entity("contracts.storage_contracts.db_models.Faculty", b =>
                {
                    b.Navigation("departments");
                });

            modelBuilder.Entity("contracts.storage_contracts.db_models.StudentGroup", b =>
                {
                    b.Navigation("students");
                });

            modelBuilder.Entity("contracts.storage_contracts.db_models.Template", b =>
                {
                    b.Navigation("documents");
                });

            modelBuilder.Entity("contracts.storage_contracts.db_models.User", b =>
                {
                    b.Navigation("documents");
                });
#pragma warning restore 612, 618
        }
    }
}
