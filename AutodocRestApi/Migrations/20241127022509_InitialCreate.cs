﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AutodocRestApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileTasks",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileTasks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FileAttachments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    FileTaskId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAttachments", x => x.id);
                    table.ForeignKey(
                        name: "FK_FileAttachments_FileTasks_FileTaskId",
                        column: x => x.FileTaskId,
                        principalTable: "FileTasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FileTasks",
                columns: new[] { "id", "Date", "IsCompleted", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 27, 2, 25, 9, 240, DateTimeKind.Utc).AddTicks(5279), false, "Initial Task" },
                    { 2, new DateTime(2024, 11, 26, 2, 25, 9, 240, DateTimeKind.Utc).AddTicks(5281), true, "Another Task" }
                });

            migrationBuilder.InsertData(
                table: "FileAttachments",
                columns: new[] { "id", "FileName", "FilePath", "FileSize", "FileTaskId" },
                values: new object[,]
                {
                    { 1, "example1.txt", "file1", 1024L, 1 },
                    { 2, "example2.pdf", "file2", 2048L, 1 },
                    { 3, "example3.docx", "file3", 512L, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_FileTaskId",
                table: "FileAttachments",
                column: "FileTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileAttachments");

            migrationBuilder.DropTable(
                name: "FileTasks");
        }
    }
}
