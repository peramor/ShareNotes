using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ShareNotesV3.ViewModels;

namespace ShareNotesV3.Models
{
    public partial class ShareNotes_dbContext : DbContext
    {
        public virtual DbSet<Campus> Campus { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Degree> Degree { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Lecture> Lectures { get; set; }
        public virtual DbSet<Marker> Markers { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<University> Universities { get; set; }
        public virtual DbSet<View> Views { get; set; }

        public ShareNotes_dbContext(DbContextOptions<ShareNotes_dbContext> options)
            : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Campus>(entity =>
            {
                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.University)
                    .WithMany(p => p.Campus)
                    .HasForeignKey(d => d.UniversityId)
                    .HasConstraintName("R_15");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseId)
                    .HasName("XPKCourses");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.FacultyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("R_3");
            });

            modelBuilder.Entity<Degree>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.HasKey(e => e.FacultyId)
                    .HasName("XPKFaculties");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Campus)
                    .WithMany(p => p.Faculties)
                    .HasForeignKey(d => d.CampusId)
                    .HasConstraintName("R_16");

                entity.HasOne(d => d.Degree)
                    .WithMany(p => p.Faculties)
                    .HasForeignKey(d => d.DegreeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("R_21");
            });

            modelBuilder.Entity<Lecture>(entity =>
            {
                entity.HasKey(e => e.LectureId)
                    .HasName("XPKLectures");

                entity.Property(e => e.Abstract).HasMaxLength(500);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Topic)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Lectures)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("R_4");
            });

            modelBuilder.Entity<Marker>(entity =>
            {
                entity.HasKey(e => e.MarkerId)
                    .HasName("XPKMarkers");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Note)
                    .WithMany(p => p.Markers)
                    .HasForeignKey(d => d.NoteId)
                    .HasConstraintName("R_24");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Markers)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("R_23");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(e => e.NoteId)
                    .HasName("XPKNotes");

                entity.Property(e => e.Document)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.LectureId)
                    .HasConstraintName("R_12");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("R_22");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentId)
                    .HasName("XPKStudents");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.Mail).HasColumnType("varchar(50)");

                entity.Property(e => e.Pwd).HasColumnType("varchar(36)");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.FacultyId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("R_17");
            });

            modelBuilder.Entity<University>(entity =>
            {
                entity.HasKey(e => e.UniversityId)
                    .HasName("XPKUniversities");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ShortName).HasMaxLength(10);
            });

            modelBuilder.Entity<View>(entity =>
            {
                entity.HasKey(e => new { e.NoteId, e.StudentId })
                    .HasName("XPKViews");

                entity.HasOne(d => d.Note)
                    .WithMany(p => p.Views)
                    .HasForeignKey(d => d.NoteId)
                    .HasConstraintName("R_13");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Views)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("R_14");
            });
        }
        public DbSet<ShareNotesV3.ViewModels.User> User { get; set; }
    }
}