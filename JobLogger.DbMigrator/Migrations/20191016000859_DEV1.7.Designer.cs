﻿// <auto-generated />
using System;
using JobLogger.DbMigrator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JobLogger.DbMigrator.Migrations
{
    [DbContext(typeof(DbMigratorContext))]
    [Migration("20191016000859_DEV1.7")]
    partial class DEV17
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JobLogger.DAL.CheckIn", b =>
                {
                    b.Property<long>("ID");

                    b.Property<DateTime>("CheckInTime");

                    b.Property<long>("CodeBranchID");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<long?>("TaskLogID");

                    b.HasKey("ID");

                    b.HasIndex("CodeBranchID");

                    b.HasIndex("TaskLogID");

                    b.ToTable("CheckIn");
                });

            modelBuilder.Entity("JobLogger.DAL.CodeBranch", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("CodeBranch");
                });

            modelBuilder.Entity("JobLogger.DAL.Feature", b =>
                {
                    b.Property<long>("ID");

                    b.Property<int>("Status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("ID");

                    b.ToTable("Feature");
                });

            modelBuilder.Entity("JobLogger.DAL.Requirement", b =>
                {
                    b.Property<long>("ID");

                    b.Property<long?>("FeatureID");

                    b.Property<int>("Status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("ID");

                    b.HasIndex("FeatureID");

                    b.ToTable("Requirement");
                });

            modelBuilder.Entity("JobLogger.DAL.RequirementComment", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .IsRequired();

                    b.Property<long>("RequirementID");

                    b.HasKey("ID");

                    b.HasIndex("RequirementID");

                    b.ToTable("RequirementComment");
                });

            modelBuilder.Entity("JobLogger.DAL.Task", b =>
                {
                    b.Property<long>("ID");

                    b.Property<bool>("IsActive");

                    b.Property<long?>("RequirementID");

                    b.Property<int>("TaskType");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("ID");

                    b.HasIndex("RequirementID");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("JobLogger.DAL.TaskCheckIn", b =>
                {
                    b.Property<long>("TaskID");

                    b.Property<long>("CheckInID");

                    b.HasKey("TaskID", "CheckInID");

                    b.HasIndex("CheckInID");

                    b.ToTable("TaskCheckIn");
                });

            modelBuilder.Entity("JobLogger.DAL.TaskComment", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .IsRequired();

                    b.Property<long>("TaskID");

                    b.HasKey("ID");

                    b.HasIndex("TaskID");

                    b.ToTable("TaskComment");
                });

            modelBuilder.Entity("JobLogger.DAL.TaskLog", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<TimeSpan?>("EndTime");

                    b.Property<DateTime>("LogDate")
                        .HasColumnType("Date");

                    b.Property<TimeSpan>("StartTime");

                    b.Property<long?>("TaskID");

                    b.Property<long?>("TimeLineID");

                    b.HasKey("ID");

                    b.HasIndex("TaskID");

                    b.HasIndex("TimeLineID");

                    b.ToTable("TaskLog");
                });

            modelBuilder.Entity("JobLogger.DAL.TaskLogComment", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .IsRequired();

                    b.Property<long>("TaskLogID");

                    b.HasKey("ID");

                    b.HasIndex("TaskLogID");

                    b.ToTable("TaskLogComment");
                });

            modelBuilder.Entity("JobLogger.DAL.TimeLine", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("ID");

                    b.ToTable("TimeLine");
                });

            modelBuilder.Entity("JobLogger.DAL.CheckIn", b =>
                {
                    b.HasOne("JobLogger.DAL.CodeBranch", "CodeBranch")
                        .WithMany("BranchCheckIns")
                        .HasForeignKey("CodeBranchID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("JobLogger.DAL.TaskLog", "TaskLog")
                        .WithMany("CheckIns")
                        .HasForeignKey("TaskLogID");
                });

            modelBuilder.Entity("JobLogger.DAL.Requirement", b =>
                {
                    b.HasOne("JobLogger.DAL.Feature", "Feature")
                        .WithMany("Requirements")
                        .HasForeignKey("FeatureID");
                });

            modelBuilder.Entity("JobLogger.DAL.RequirementComment", b =>
                {
                    b.HasOne("JobLogger.DAL.Requirement")
                        .WithMany("Comments")
                        .HasForeignKey("RequirementID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JobLogger.DAL.Task", b =>
                {
                    b.HasOne("JobLogger.DAL.Requirement", "Requirement")
                        .WithMany("Tasks")
                        .HasForeignKey("RequirementID");
                });

            modelBuilder.Entity("JobLogger.DAL.TaskCheckIn", b =>
                {
                    b.HasOne("JobLogger.DAL.CheckIn")
                        .WithMany("TaskCheckIns")
                        .HasForeignKey("CheckInID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("JobLogger.DAL.Task")
                        .WithMany("CheckIns")
                        .HasForeignKey("TaskID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JobLogger.DAL.TaskComment", b =>
                {
                    b.HasOne("JobLogger.DAL.Task")
                        .WithMany("Comments")
                        .HasForeignKey("TaskID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("JobLogger.DAL.TaskLog", b =>
                {
                    b.HasOne("JobLogger.DAL.Task", "Task")
                        .WithMany("Logs")
                        .HasForeignKey("TaskID");

                    b.HasOne("JobLogger.DAL.TimeLine", "TimeLine")
                        .WithMany("TaskLogs")
                        .HasForeignKey("TimeLineID");
                });

            modelBuilder.Entity("JobLogger.DAL.TaskLogComment", b =>
                {
                    b.HasOne("JobLogger.DAL.TaskLog")
                        .WithMany("Comments")
                        .HasForeignKey("TaskLogID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
