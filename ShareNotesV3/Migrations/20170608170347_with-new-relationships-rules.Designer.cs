using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ShareNotesV3.Models;

namespace ShareNotesV3.Migrations
{
    [DbContext(typeof(ShareNotes_dbContext))]
    [Migration("20170608170347_with-new-relationships-rules")]
    partial class withnewrelationshipsrules
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ShareNotesV3.Models.Campus", b =>
                {
                    b.Property<int>("CampusId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("UniversityId");

                    b.HasKey("CampusId");

                    b.HasIndex("UniversityId");

                    b.ToTable("Campus");
                });

            modelBuilder.Entity("ShareNotesV3.Models.Courses", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("FacultyId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("CourseId")
                        .HasName("XPKCourses");

                    b.HasIndex("FacultyId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("ShareNotesV3.Models.Degree", b =>
                {
                    b.Property<int>("DegreeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("DegreeId");

                    b.ToTable("Degree");
                });

            modelBuilder.Entity("ShareNotesV3.Models.Faculties", b =>
                {
                    b.Property<int>("FacultyId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CampusId");

                    b.Property<int?>("DegreeId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("FacultyId")
                        .HasName("XPKFaculties");

                    b.HasIndex("CampusId");

                    b.HasIndex("DegreeId");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("ShareNotesV3.Models.Lectures", b =>
                {
                    b.Property<int>("LectureId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abstract")
                        .HasMaxLength(500);

                    b.Property<int>("CourseId");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("LectureId")
                        .HasName("XPKLectures");

                    b.HasIndex("CourseId");

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("ShareNotesV3.Models.Markers", b =>
                {
                    b.Property<int>("MarkerId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EndY");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("NoteId");

                    b.Property<int>("StartY");

                    b.Property<int>("StudentId");

                    b.HasKey("MarkerId")
                        .HasName("XPKMarkers");

                    b.HasIndex("NoteId");

                    b.HasIndex("StudentId");

                    b.ToTable("Markers");
                });

            modelBuilder.Entity("ShareNotesV3.Models.Notes", b =>
                {
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("LectureId");

                    b.Property<int?>("StudentId");

                    b.HasKey("NoteId")
                        .HasName("XPKNotes");

                    b.HasIndex("LectureId");

                    b.HasIndex("StudentId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("ShareNotesV3.Models.Students", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("FacultyId");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<string>("Mail")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Pwd")
                        .HasColumnType("varchar(36)");

                    b.HasKey("StudentId")
                        .HasName("XPKStudents");

                    b.HasIndex("FacultyId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("ShareNotesV3.Models.Universities", b =>
                {
                    b.Property<int>("UniversityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ShortName")
                        .HasMaxLength(10);

                    b.HasKey("UniversityId")
                        .HasName("XPKUniversities");

                    b.ToTable("Universities");
                });

            modelBuilder.Entity("ShareNotesV3.Models.Views", b =>
                {
                    b.Property<int>("NoteId");

                    b.Property<int>("StudentId");

                    b.Property<bool?>("Favorite");

                    b.Property<int?>("Rate");

                    b.HasKey("NoteId", "StudentId")
                        .HasName("XPKViews");

                    b.HasIndex("StudentId");

                    b.ToTable("Views");
                });

            modelBuilder.Entity("ShareNotesV3.Models.Campus", b =>
                {
                    b.HasOne("ShareNotesV3.Models.Universities", "University")
                        .WithMany("Campus")
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ShareNotesV3.Models.Courses", b =>
                {
                    b.HasOne("ShareNotesV3.Models.Faculties", "Faculty")
                        .WithMany("Courses")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ShareNotesV3.Models.Faculties", b =>
                {
                    b.HasOne("ShareNotesV3.Models.Campus", "Campus")
                        .WithMany("Faculties")
                        .HasForeignKey("CampusId")
                        .HasConstraintName("R_16")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ShareNotesV3.Models.Degree", "Degree")
                        .WithMany("Faculties")
                        .HasForeignKey("DegreeId")
                        .HasConstraintName("R_21")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("ShareNotesV3.Models.Lectures", b =>
                {
                    b.HasOne("ShareNotesV3.Models.Courses", "Course")
                        .WithMany("Lectures")
                        .HasForeignKey("CourseId")
                        .HasConstraintName("R_4")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ShareNotesV3.Models.Markers", b =>
                {
                    b.HasOne("ShareNotesV3.Models.Notes", "Note")
                        .WithMany("Markers")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ShareNotesV3.Models.Students", "Student")
                        .WithMany("Markers")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ShareNotesV3.Models.Notes", b =>
                {
                    b.HasOne("ShareNotesV3.Models.Lectures", "Lecture")
                        .WithMany("Notes")
                        .HasForeignKey("LectureId")
                        .HasConstraintName("R_12")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ShareNotesV3.Models.Students", "Student")
                        .WithMany("Notes")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("ShareNotesV3.Models.Students", b =>
                {
                    b.HasOne("ShareNotesV3.Models.Faculties", "Faculty")
                        .WithMany("Students")
                        .HasForeignKey("FacultyId")
                        .HasConstraintName("R_17")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("ShareNotesV3.Models.Views", b =>
                {
                    b.HasOne("ShareNotesV3.Models.Notes", "Note")
                        .WithMany("Views")
                        .HasForeignKey("NoteId")
                        .HasConstraintName("R_13")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ShareNotesV3.Models.Students", "Student")
                        .WithMany("Views")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("R_14")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
