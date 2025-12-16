using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoginTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Websites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HostUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublish = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Websites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Websites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsitesId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pages_Websites_WebsitesId",
                        column: x => x.WebsitesId,
                        principalTable: "Websites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    PageModelId = table.Column<int>(type: "int", nullable: true),
                    MarginMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaddingMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Padding = table.Column<int>(type: "int", nullable: true),
                    PaddingTop = table.Column<int>(type: "int", nullable: true),
                    PaddingBottom = table.Column<int>(type: "int", nullable: true),
                    PaddingLeft = table.Column<int>(type: "int", nullable: true),
                    PaddingRight = table.Column<int>(type: "int", nullable: true),
                    Margin = table.Column<int>(type: "int", nullable: true),
                    MarginTop = table.Column<int>(type: "int", nullable: true),
                    MarginBottom = table.Column<int>(type: "int", nullable: true),
                    MarginLeft = table.Column<int>(type: "int", nullable: true),
                    MarginRight = table.Column<int>(type: "int", nullable: true),
                    BackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BorderRadius = table.Column<int>(type: "int", nullable: true),
                    BoxShadow = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Border = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BorderTop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BorderBottom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BorderWidth = table.Column<int>(type: "int", nullable: true),
                    BorderColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BorderStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opacity = table.Column<int>(type: "int", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FontSize = table.Column<int>(type: "int", nullable: true),
                    FontWeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextAlign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FontFamily = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FontStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LineHeight = table.Column<float>(type: "real", nullable: true),
                    LetterSpacing = table.Column<int>(type: "int", nullable: true),
                    TextShadow = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextDecoration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhiteSpace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverflowWrap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Width = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<int>(type: "int", nullable: true),
                    MinWidth = table.Column<int>(type: "int", nullable: true),
                    MaxWidth = table.Column<int>(type: "int", nullable: true),
                    MinHeight = table.Column<int>(type: "int", nullable: true),
                    MaxHeight = table.Column<int>(type: "int", nullable: true),
                    Display = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JustifyContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlignItems = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlignContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gap = table.Column<int>(type: "int", nullable: true),
                    FlexDirection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JustifyItems = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Overflow = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverflowX = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverflowY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Top = table.Column<int>(type: "int", nullable: true),
                    Left = table.Column<int>(type: "int", nullable: true),
                    Right = table.Column<int>(type: "int", nullable: true),
                    Bottom = table.Column<int>(type: "int", nullable: true),
                    ZIndex = table.Column<int>(type: "int", nullable: true),
                    Cursor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transform = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GridTemplateRows = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GridTemplateColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundRepeat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundPosition = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Pages_PageModelId",
                        column: x => x.PageModelId,
                        principalTable: "Pages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Columns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Columns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Columns_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColumnId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Href = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ListStyleType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Target = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Orientation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thickness = table.Column<int>(type: "int", nullable: true),
                    IconName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Poster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Volume = table.Column<double>(type: "float", nullable: true),
                    PlaybackRate = table.Column<bool>(type: "bit", nullable: true),
                    Controls = table.Column<bool>(type: "bit", nullable: true),
                    Muted = table.Column<bool>(type: "bit", nullable: true),
                    Loop = table.Column<bool>(type: "bit", nullable: true),
                    Autoplay = table.Column<bool>(type: "bit", nullable: true),
                    MarginMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaddingMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Padding = table.Column<int>(type: "int", nullable: true),
                    PaddingTop = table.Column<int>(type: "int", nullable: true),
                    PaddingBottom = table.Column<int>(type: "int", nullable: true),
                    PaddingLeft = table.Column<int>(type: "int", nullable: true),
                    PaddingRight = table.Column<int>(type: "int", nullable: true),
                    Margin = table.Column<int>(type: "int", nullable: true),
                    MarginTop = table.Column<int>(type: "int", nullable: true),
                    MarginBottom = table.Column<int>(type: "int", nullable: true),
                    MarginLeft = table.Column<int>(type: "int", nullable: true),
                    MarginRight = table.Column<int>(type: "int", nullable: true),
                    BackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BorderRadius = table.Column<int>(type: "int", nullable: true),
                    BoxShadow = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Border = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BorderTop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BorderBottom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BorderWidth = table.Column<int>(type: "int", nullable: true),
                    BorderColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BorderStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opacity = table.Column<int>(type: "int", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FontSize = table.Column<int>(type: "int", nullable: true),
                    FontWeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextAlign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FontFamily = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FontStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LineHeight = table.Column<float>(type: "real", nullable: true),
                    LetterSpacing = table.Column<int>(type: "int", nullable: true),
                    TextShadow = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextDecoration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhiteSpace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverflowWrap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Width = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<int>(type: "int", nullable: true),
                    MinWidth = table.Column<int>(type: "int", nullable: true),
                    MaxWidth = table.Column<int>(type: "int", nullable: true),
                    MinHeight = table.Column<int>(type: "int", nullable: true),
                    MaxHeight = table.Column<int>(type: "int", nullable: true),
                    Display = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JustifyContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlignItems = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlignContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gap = table.Column<int>(type: "int", nullable: true),
                    FlexDirection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JustifyItems = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Overflow = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverflowX = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverflowY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Top = table.Column<int>(type: "int", nullable: true),
                    Left = table.Column<int>(type: "int", nullable: true),
                    Right = table.Column<int>(type: "int", nullable: true),
                    Bottom = table.Column<int>(type: "int", nullable: true),
                    ZIndex = table.Column<int>(type: "int", nullable: true),
                    Cursor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transform = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GridTemplateRows = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GridTemplateColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundRepeat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundPosition = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slots_Columns_ColumnId",
                        column: x => x.ColumnId,
                        principalTable: "Columns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Href = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Target = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SlotsId = table.Column<int>(type: "int", nullable: false),
                    MarginMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaddingMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Padding = table.Column<int>(type: "int", nullable: true),
                    PaddingTop = table.Column<int>(type: "int", nullable: true),
                    PaddingBottom = table.Column<int>(type: "int", nullable: true),
                    PaddingLeft = table.Column<int>(type: "int", nullable: true),
                    PaddingRight = table.Column<int>(type: "int", nullable: true),
                    Margin = table.Column<int>(type: "int", nullable: true),
                    MarginTop = table.Column<int>(type: "int", nullable: true),
                    MarginBottom = table.Column<int>(type: "int", nullable: true),
                    MarginLeft = table.Column<int>(type: "int", nullable: true),
                    MarginRight = table.Column<int>(type: "int", nullable: true),
                    BackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BorderRadius = table.Column<int>(type: "int", nullable: true),
                    BoxShadow = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Border = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BorderTop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BorderBottom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BorderWidth = table.Column<int>(type: "int", nullable: true),
                    BorderColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BorderStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opacity = table.Column<int>(type: "int", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FontSize = table.Column<int>(type: "int", nullable: true),
                    FontWeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextAlign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FontFamily = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FontStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LineHeight = table.Column<float>(type: "real", nullable: true),
                    LetterSpacing = table.Column<int>(type: "int", nullable: true),
                    TextShadow = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextDecoration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhiteSpace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverflowWrap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Width = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<int>(type: "int", nullable: true),
                    MinWidth = table.Column<int>(type: "int", nullable: true),
                    MaxWidth = table.Column<int>(type: "int", nullable: true),
                    MinHeight = table.Column<int>(type: "int", nullable: true),
                    MaxHeight = table.Column<int>(type: "int", nullable: true),
                    Display = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JustifyContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlignItems = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlignContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gap = table.Column<int>(type: "int", nullable: true),
                    FlexDirection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JustifyItems = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Overflow = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverflowX = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverflowY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Top = table.Column<int>(type: "int", nullable: true),
                    Left = table.Column<int>(type: "int", nullable: true),
                    Right = table.Column<int>(type: "int", nullable: true),
                    Bottom = table.Column<int>(type: "int", nullable: true),
                    ZIndex = table.Column<int>(type: "int", nullable: true),
                    Cursor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transform = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GridTemplateRows = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GridTemplateColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundRepeat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundPosition = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListItems_Slots_SlotsId",
                        column: x => x.SlotsId,
                        principalTable: "Slots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Columns_SectionId",
                table: "Columns",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ListItems_SlotsId",
                table: "ListItems",
                column: "SlotsId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_WebsitesId",
                table: "Pages",
                column: "WebsitesId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_PageModelId",
                table: "Sections",
                column: "PageModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_ColumnId",
                table: "Slots",
                column: "ColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_Websites_UserId",
                table: "Websites",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ListItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Slots");

            migrationBuilder.DropTable(
                name: "Columns");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Websites");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
