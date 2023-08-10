using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace M7CarManager.Migrations
{
    public partial class seed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0d2bd190-5664-4ef8-b267-c9a2608a1e9e");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoContentType", "PhotoData", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2fbe09ca-62d9-4964-94a8-6f1ad0693139", 0, "9f203034-d679-4fe6-940e-49e12a4bac01", "kovi91@gmail.com", true, "Kovács", "András", false, null, null, "KOVI91@GMAIL.COM", "AQAAAAEAACcQAAAAECjwKgtuoXO/XVHJsifuJNvY+DzlCxJVQK3z5Dmp8h9aExWb9Sx1HmdlLVf1W9AUFQ==", null, false, null, null, "bf0bd893-070c-4268-b863-2f0b4c212196", false, "kovi91@gmail.com" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Model", "OwnerId", "PlateNumber", "Price" },
                values: new object[] { "891b3fdc-9a7f-4573-8e67-6256a6ef0585", "Opel Astra", "2fbe09ca-62d9-4964-94a8-6f1ad0693139", "ABC-123", 2000 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: "891b3fdc-9a7f-4573-8e67-6256a6ef0585");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2fbe09ca-62d9-4964-94a8-6f1ad0693139");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoContentType", "PhotoData", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0d2bd190-5664-4ef8-b267-c9a2608a1e9e", 0, "4205ee8e-9815-44d5-8af9-13211ecfc479", "kovi91@gmail.com", true, "Kovács", "András", false, null, null, "KOVI91@GMAIL.COM", "AQAAAAEAACcQAAAAEAyuPFypPKKjseSNY2T7Oj6rh9b9MXtTRZTmE2UmR4VmPeH/FKxCGii/P2VwXoxs4A==", null, false, null, null, "b8ee42bb-d690-4180-b33c-65d66e6e5b26", false, "kovi91@gmail.com" });
        }
    }
}
