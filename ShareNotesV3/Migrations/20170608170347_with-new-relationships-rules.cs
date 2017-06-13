using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShareNotesV3.Migrations
{
    public partial class withnewrelationshipsrules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campus_Universities_UniversityId",
                table: "Campus");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Faculties_FacultyId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "R_16",
                table: "Faculties");

            migrationBuilder.DropForeignKey(
                name: "R_21",
                table: "Faculties");

            migrationBuilder.DropForeignKey(
                name: "R_4",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "R_5",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Markers_Notes_NoteId",
                table: "Markers");

            migrationBuilder.DropForeignKey(
                name: "FK_Markers_Students_StudentId",
                table: "Markers");

            migrationBuilder.DropForeignKey(
                name: "R_12",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Students_StudentId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "R_17",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "R_13",
                table: "Views");

            migrationBuilder.DropForeignKey(
                name: "R_14",
                table: "Views");

            migrationBuilder.DropTable(
                name: "Lecturers");

            migrationBuilder.DropIndex(
                name: "IX_Lectures_LecturerId",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "LecturerId",
                table: "Lectures");

            migrationBuilder.AlterColumn<int>(
                name: "FacultyId",
                table: "Students",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Notes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Abstract",
                table: "Lectures",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Faculties",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Degree",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<int>(
                name: "FacultyId",
                table: "Courses",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Campus_Universities_UniversityId",
                table: "Campus",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Faculties_FacultyId",
                table: "Courses",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "FacultyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "R_16",
                table: "Faculties",
                column: "CampusId",
                principalTable: "Campus",
                principalColumn: "CampusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "R_21",
                table: "Faculties",
                column: "DegreeId",
                principalTable: "Degree",
                principalColumn: "DegreeId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "R_4",
                table: "Lectures",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_Notes_NoteId",
                table: "Markers",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_Students_StudentId",
                table: "Markers",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "R_12",
                table: "Notes",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "LectureId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Students_StudentId",
                table: "Notes",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "R_17",
                table: "Students",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "FacultyId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "R_13",
                table: "Views",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "R_14",
                table: "Views",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campus_Universities_UniversityId",
                table: "Campus");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Faculties_FacultyId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "R_16",
                table: "Faculties");

            migrationBuilder.DropForeignKey(
                name: "R_21",
                table: "Faculties");

            migrationBuilder.DropForeignKey(
                name: "R_4",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Markers_Notes_NoteId",
                table: "Markers");

            migrationBuilder.DropForeignKey(
                name: "FK_Markers_Students_StudentId",
                table: "Markers");

            migrationBuilder.DropForeignKey(
                name: "R_12",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Students_StudentId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "R_17",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "R_13",
                table: "Views");

            migrationBuilder.DropForeignKey(
                name: "R_14",
                table: "Views");

            migrationBuilder.AlterColumn<int>(
                name: "FacultyId",
                table: "Students",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Notes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Abstract",
                table: "Lectures",
                type: "varchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LecturerId",
                table: "Lectures",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Faculties",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Degree",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "FacultyId",
                table: "Courses",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    LecturerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(maxLength: 50, nullable: true),
                    Mail = table.Column<string>(type: "varchar(30)", nullable: true),
                    PhotoLink = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("XPKLecturers", x => x.LecturerId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_LecturerId",
                table: "Lectures",
                column: "LecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campus_Universities_UniversityId",
                table: "Campus",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Faculties_FacultyId",
                table: "Courses",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "FacultyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "R_16",
                table: "Faculties",
                column: "CampusId",
                principalTable: "Campus",
                principalColumn: "CampusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "R_21",
                table: "Faculties",
                column: "DegreeId",
                principalTable: "Degree",
                principalColumn: "DegreeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "R_4",
                table: "Lectures",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "R_5",
                table: "Lectures",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "LecturerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_Notes_NoteId",
                table: "Markers",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_Students_StudentId",
                table: "Markers",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "R_12",
                table: "Notes",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "LectureId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Students_StudentId",
                table: "Notes",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "R_17",
                table: "Students",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "FacultyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "R_13",
                table: "Views",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "R_14",
                table: "Views",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
