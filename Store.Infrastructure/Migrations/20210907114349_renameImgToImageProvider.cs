using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Infrastructure.Migrations
{
    public partial class renameImgToImageProvider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImgPath",
                table: "Providers",
                newName: "ImagePath");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2301D884-221A-4E7D-B509-0113DCC043E1",
                column: "ConcurrencyStamp",
                value: "37b5be16-7406-4a66-98e0-72d0c10addc0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7D9B7113-A8F8-4035-99A7-A20DD400F6A3",
                column: "ConcurrencyStamp",
                value: "5b957453-13ac-4e94-bc1f-d52a8b083edf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B22698B8-42A2-4115-9631-1C2D1E2AC5F7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d4265664-f25b-420a-9aad-9097232ebd2c", "AQAAAAEAACcQAAAAEH2SL+Xspj+c1H6sYmv+M1tioFfEu915nMnV+ngSMwshqzNs4LhlWCvbgJYe0dHAXA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Providers",
                newName: "ImgPath");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2301D884-221A-4E7D-B509-0113DCC043E1",
                column: "ConcurrencyStamp",
                value: "9e901fec-2677-4149-8814-671fabe8e79d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7D9B7113-A8F8-4035-99A7-A20DD400F6A3",
                column: "ConcurrencyStamp",
                value: "e94cd78e-1dd6-4181-8661-183f611544ca");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B22698B8-42A2-4115-9631-1C2D1E2AC5F7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dca5497b-fd64-47f1-828c-236a6bdf8c04", "AQAAAAEAACcQAAAAECZtzFPDnV/16Z3kBLeAmkApDCxk2KuhW0DE22vVbHhiUCsAMYBitgn/SMCC3Oyg1w==" });
        }
    }
}
