using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace M7CarManager.Migrations
{
    public partial class notreq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: "891b3fdc-9a7f-4573-8e67-6256a6ef0585");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2fbe09ca-62d9-4964-94a8-6f1ad0693139");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Cars",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoContentType", "PhotoData", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c64ecd58-07de-46aa-b28e-67bc1004c74b", 0, "a324f949-00dd-4cb3-b4b7-1485e1014e89", "kovi91@gmail.com", true, "Kovács", "András", false, null, null, "KOVI91@GMAIL.COM", "AQAAAAEAACcQAAAAEO6XzivLt3cydYLKjEYDDM1IA71/JinhQW7CqsthDJpeyjZcWPUtH6AmG4EoexJPKg==", null, false, null, null, "9f35cf4b-7a5d-4a55-bb31-69f284b44005", false, "kovi91@gmail.com" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Model", "OwnerId", "PlateNumber", "Price" },
                values: new object[] { "f4517e21-88f0-494e-a557-5178b52bccfb", "Opel Astra", "c64ecd58-07de-46aa-b28e-67bc1004c74b", "ABC-123", 2000 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: "f4517e21-88f0-494e-a557-5178b52bccfb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c64ecd58-07de-46aa-b28e-67bc1004c74b");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Cars",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoContentType", "PhotoData", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2fbe09ca-62d9-4964-94a8-6f1ad0693139", 0, "9f203034-d679-4fe6-940e-49e12a4bac01", "kovi91@gmail.com", true, "Kovács", "András", false, null, null, "KOVI91@GMAIL.COM", "AQAAAAEAACcQAAAAECjwKgtuoXO/XVHJsifuJNvY+DzlCxJVQK3z5Dmp8h9aExWb9Sx1HmdlLVf1W9AUFQ==", null, false, null, null, "bf0bd893-070c-4268-b863-2f0b4c212196", false, "kovi91@gmail.com" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Model", "OwnerId", "PlateNumber", "Price" },
                values: new object[] { "891b3fdc-9a7f-4573-8e67-6256a6ef0585", "Opel Astra", "2fbe09ca-62d9-4964-94a8-6f1ad0693139", "ABC-123", 2000 });
        }
    }
}
