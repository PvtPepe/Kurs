using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicAppDAL.EF.ClinicMigrations
{
    public partial class NamesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patient_PatientFirstName_PatientMidName_PatientLastName",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_DocFirstName_DocMidName_DocLastName",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "PatientFirstName",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PatientLastName",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PatientMidName",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "DocFirstName",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "DocLastName",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "DocMidName",
                table: "Doctor");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Patient",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Patient",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MidName",
                table: "Patient",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Doctor",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Doctor",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MidName",
                table: "Doctor",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_FirstName_MidName_LastName",
                table: "Patient",
                columns: new[] { "FirstName", "MidName", "LastName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_FirstName_MidName_LastName",
                table: "Doctor",
                columns: new[] { "FirstName", "MidName", "LastName" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patient_FirstName_MidName_LastName",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_FirstName_MidName_LastName",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "MidName",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "MidName",
                table: "Doctor");

            migrationBuilder.AddColumn<string>(
                name: "PatientFirstName",
                table: "Patient",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PatientLastName",
                table: "Patient",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PatientMidName",
                table: "Patient",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DocFirstName",
                table: "Doctor",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DocLastName",
                table: "Doctor",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DocMidName",
                table: "Doctor",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PatientFirstName_PatientMidName_PatientLastName",
                table: "Patient",
                columns: new[] { "PatientFirstName", "PatientMidName", "PatientLastName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_DocFirstName_DocMidName_DocLastName",
                table: "Doctor",
                columns: new[] { "DocFirstName", "DocMidName", "DocLastName" },
                unique: true);
        }
    }
}
