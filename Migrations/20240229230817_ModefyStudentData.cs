using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_FinalTask.Migrations
{
    /// <inheritdoc />
    public partial class ModefyStudentData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DepartmentId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_DepartmentId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "DeptID",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Students_DeptID",
                table: "Students",
                column: "DeptID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DeptID",
                table: "Students",
                column: "DeptID",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DeptID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_DeptID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DeptID",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentId",
                table: "Students",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DepartmentId",
                table: "Students",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
