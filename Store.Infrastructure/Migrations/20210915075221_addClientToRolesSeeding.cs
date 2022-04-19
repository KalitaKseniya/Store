using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Infrastructure.Migrations
{
    public partial class addClientToRolesSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2301D884-221A-4E7D-B509-0113DCC043E1",
                column: "ConcurrencyStamp",
                value: "5a7f4ec8-8266-4234-9467-54bab14e0b0c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7D9B7113-A8F8-4035-99A7-A20DD400F6A3",
                column: "ConcurrencyStamp",
                value: "895d453f-caad-489c-8ea0-8dbbf468c520");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ab9981f8-626b-4f21-be32-25a716c6d939", "918da7a6-b914-4158-894c-089257e94b36", "Client", "CLIENT" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B22698B8-42A2-4115-9631-1C2D1E2AC5F7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7ad58226-4556-4a53-81d9-cd2563c5bfa1", "AQAAAAEAACcQAAAAECgV86fT/nOBHZtzPSrSNbh1PvZjLKxk72/78bi3ZRQHzvuUtz9Ky1RguEyOUjU1tg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab9981f8-626b-4f21-be32-25a716c6d939");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2301D884-221A-4E7D-B509-0113DCC043E1",
                column: "ConcurrencyStamp",
                value: "2faf5374-6aea-4b66-9bd1-140ffb9044b8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7D9B7113-A8F8-4035-99A7-A20DD400F6A3",
                column: "ConcurrencyStamp",
                value: "ab45e1ca-cbcb-4a0d-8a67-494b7b10d3f1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B22698B8-42A2-4115-9631-1C2D1E2AC5F7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6a5fedff-0311-4257-ae1d-76cec0adc68e", "AQAAAAEAACcQAAAAEH2gcev0krfAZKYc14CjIQJpIKQYzQ7eJe/PXPbTSGZuliMba6LjElEJVusZk/2xBg==" });
        }
    }
}
