using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silvernet.Migrations
{
    /// <inheritdoc />
    public partial class idfix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // This migration removes IDENTITY from Id columns
            // WARNING: This will delete all existing data!
            
            // Option 1: If you want to keep data, you need to manually assign valid Israeli IDs
            // Option 2: If data is not important, we'll recreate tables from scratch
            
            migrationBuilder.Sql(@"
                -- Drop foreign key
                ALTER TABLE Users DROP CONSTRAINT FK_Users_Tenants_TenantId;
                
                -- Backup data if needed (optional)
                -- SELECT * INTO Users_Backup FROM Users;
                -- SELECT * INTO Tenants_Backup FROM Tenants;
                
                -- Drop tables and recreate without IDENTITY
                DROP TABLE Users;
                DROP TABLE Tenants;
                
                -- Recreate Tenants table without IDENTITY
                CREATE TABLE Tenants (
                    Id bigint NOT NULL PRIMARY KEY,
                    Name nvarchar(100) NOT NULL,
                    Email nvarchar(256) NOT NULL,
                    Phone nvarchar(20) NOT NULL,
                    CreationDate datetime2 NOT NULL DEFAULT GETDATE()
                );
                
                -- Recreate indexes on Tenants
                CREATE UNIQUE INDEX IX_Tenants_Email ON Tenants (Email);
                CREATE UNIQUE INDEX IX_Tenants_Phone ON Tenants (Phone);
                
                -- Recreate Users table without IDENTITY
                CREATE TABLE Users (
                    Id bigint NOT NULL PRIMARY KEY,
                    FirstName nvarchar(50) NOT NULL,
                    LastName nvarchar(50) NOT NULL,
                    Phone nvarchar(50) NOT NULL,
                    Email nvarchar(256) NOT NULL,
                    CreationDate datetime2 NOT NULL DEFAULT GETDATE(),
                    TenantId bigint NOT NULL,
                    CONSTRAINT FK_Users_Tenants_TenantId FOREIGN KEY (TenantId) 
                        REFERENCES Tenants (Id) ON DELETE CASCADE
                );
                
                -- Recreate indexes on Users
                CREATE UNIQUE INDEX IX_Users_Email ON Users (Email);
                CREATE UNIQUE INDEX IX_Users_Phone ON Users (Phone);
                CREATE INDEX IX_Users_TenantId ON Users (TenantId);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                -- Drop foreign key
                ALTER TABLE Users DROP CONSTRAINT FK_Users_Tenants_TenantId;
                
                -- Drop tables
                DROP TABLE Users;
                DROP TABLE Tenants;
                
                -- Recreate Tenants table WITH IDENTITY
                CREATE TABLE Tenants (
                    Id bigint IDENTITY(1,1) NOT NULL PRIMARY KEY,
                    Name nvarchar(100) NOT NULL,
                    Email nvarchar(256) NOT NULL,
                    Phone nvarchar(20) NOT NULL,
                    CreationDate datetime2 NOT NULL DEFAULT GETDATE()
                );
                
                -- Recreate indexes on Tenants
                CREATE UNIQUE INDEX IX_Tenants_Email ON Tenants (Email);
                CREATE UNIQUE INDEX IX_Tenants_Phone ON Tenants (Phone);
                
                -- Recreate Users table WITH IDENTITY
                CREATE TABLE Users (
                    Id bigint IDENTITY(1,1) NOT NULL PRIMARY KEY,
                    FirstName nvarchar(50) NOT NULL,
                    LastName nvarchar(50) NOT NULL,
                    Phone nvarchar(50) NOT NULL,
                    Email nvarchar(256) NOT NULL,
                    CreationDate datetime2 NOT NULL DEFAULT GETDATE(),
                    TenantId bigint NOT NULL,
                    CONSTRAINT FK_Users_Tenants_TenantId FOREIGN KEY (TenantId) 
                        REFERENCES Tenants (Id) ON DELETE CASCADE
                );
                
                -- Recreate indexes on Users
                CREATE UNIQUE INDEX IX_Users_Email ON Users (Email);
                CREATE UNIQUE INDEX IX_Users_Phone ON Users (Phone);
                CREATE INDEX IX_Users_TenantId ON Users (TenantId);
            ");
        }
    }
}
