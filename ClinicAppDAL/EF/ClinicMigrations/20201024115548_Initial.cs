using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicAppDAL.EF.ClinicMigrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    DiagnosisName = table.Column<string>(maxLength: 50, nullable: false),
                    DiagnosisTreatment = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    DocFirstName = table.Column<string>(maxLength: 50, nullable: false),
                    DocLastName = table.Column<string>(maxLength: 50, nullable: false),
                    DocMidName = table.Column<string>(maxLength: 50, nullable: false),
                    Speciality = table.Column<string>(maxLength: 50, nullable: false),
                    LengthOfService = table.Column<string>(maxLength: 50, nullable: false),
                    DocNumber = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    PatientFirstName = table.Column<string>(maxLength: 50, nullable: false),
                    PatientLastName = table.Column<string>(maxLength: 50, nullable: false),
                    PatientMidName = table.Column<string>(maxLength: 50, nullable: false),
                    PatientBirthdate = table.Column<DateTime>(type: "date", nullable: false),
                    PatientAdress = table.Column<string>(maxLength: 50, nullable: false),
                    PatientNumber = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorVisit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    VisitDate = table.Column<DateTime>(type: "date", nullable: false),
                    DoctorID = table.Column<int>(nullable: false),
                    PatientID = table.Column<int>(nullable: false),
                    DiagnosisID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorVisit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorVisit_Diagnoses_DiagnosisID",
                        column: x => x.DiagnosisID,
                        principalTable: "Diagnoses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorVisit_Doctor_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorVisit_Patient_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_DocFirstName_DocMidName_DocLastName",
                table: "Doctor",
                columns: new[] { "DocFirstName", "DocMidName", "DocLastName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorVisit_DiagnosisID",
                table: "DoctorVisit",
                column: "DiagnosisID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorVisit_DoctorID",
                table: "DoctorVisit",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorVisit_PatientID",
                table: "DoctorVisit",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PatientFirstName_PatientMidName_PatientLastName",
                table: "Patient",
                columns: new[] { "PatientFirstName", "PatientMidName", "PatientLastName" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorVisit");

            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patient");
        }
    }
}
