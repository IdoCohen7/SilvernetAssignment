using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silvernet.Migrations
{
    /// <inheritdoc />
    public partial class idfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraint first
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Tenants_TenantId",
                table: "Users");

            // Drop Users table primary key
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            // Drop Tenants table primary key
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants");

            // Drop and recreate Tenants.Id column without IDENTITY
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tenants");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Tenants",
                type: "bigint",
                nullable: false);

            // Drop and recreate Users.Id column without IDENTITY
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Users");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Users",
                type: "bigint",
                nullable: false);

            // Re-add primary keys
            migrationBuilder.AddPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            // Re-add foreign key constraint
            migrationBuilder.AddForeignKey(
                name: "FK_Users_Tenants_TenantId",
                table: "Users",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraint first
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Tenants_TenantId",
                table: "Users");

            // Drop Users table primary key
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            // Drop Tenants table primary key
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants");

            // Drop and recreate Tenants.Id column with IDENTITY
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tenants");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Tenants",
                type: "bigint",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            // Drop and recreate Users.Id column with IDENTITY
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Users");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Users",
                type: "bigint",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            // Re-add primary keys
            migrationBuilder.AddPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            // Re-add foreign key constraint
            migrationBuilder.AddForeignKey(
                name: "FK_Users_Tenants_TenantId",
                table: "Users",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
