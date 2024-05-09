using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Mihcelle.Hwavmvid.Server.Data;

namespace Mihcelle.Hwavmvid.Server.Migrations
{

    [DbContext(typeof(Applicationdbcontext))]
    [Migration("mihcelle.hwavmvid.01.00.00.00")]
    public class Applicationmigration : Migration
    {

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            base.BuildTargetModel(modelBuilder);
        }

        protected override void Up(MigrationBuilder migrationbuilder)
        {

            migrationbuilder.CreateTable(
                 name: "Applicationusers",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_userid", item => item.Id);
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationsites",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Name = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Description = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Brandmark = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Favicon = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_siteid", item => item.Id);
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationtenants",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Siteid = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Name = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Databaseconnectionstring = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_tenantid", item => item.Id);
                     dbtable.ForeignKey("fk_application_tenant_siteid", item => item.Siteid, "Applicationtenants", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationpages",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Siteid = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Urlpath = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Name = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Isnavigation = dbtable.Column<bool>(type: "bit", nullable: false, unicode: null),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_pageid", item => item.Id);
                     dbtable.ForeignKey("fk_application_page_siteid", item => item.Siteid, "Applicationsites", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationcontainers",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Pageid = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Containertype = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_containerid", item => item.Id);
                     dbtable.ForeignKey("fk_application_container_pageid", item => item.Pageid, "Applicationpages", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationcontainercolumns",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Containerid = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Gridposition = dbtable.Column<int>(type: "int", nullable: false, unicode: null),
                     Columnwidth = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_containercolumnid", item => item.Id);
                     dbtable.ForeignKey("fk_application_containercolumn_containerid", item => item.Containerid, "Applicationcontainers", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationmodulepackages",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),                     
                     Name = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Version = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Icon = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Assemblytype = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Settingstype = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Description = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),                     
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_modulepackageid", item => item.Id);
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationmodules",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Packageid = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Containercolumnid = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Assemblytype = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Settingstype = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Containercolumnposition = dbtable.Column<int>(type: "int", nullable: false, unicode: null),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_moduleid", item => item.Id);
                     dbtable.ForeignKey("fk_application_module_packageid", item => item.Packageid, "Applicationpackages", "Id");
                     dbtable.ForeignKey("fk_application_module_containercolumnid", item => item.Containercolumnid, "Applicationcontainercolumns", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationmodulesettings",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Moduleid = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Key = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Value = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_moduleid", item => item.Id);
                     dbtable.ForeignKey("fk_application_modulesettings_moduleid", item => item.Moduleid, "Applicationmodules", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationmediafiles",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Siteid = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Filename = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Fileextension = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Filesize = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 800),
                     Filewidth = dbtable.Column<int>(type: "int", nullable: false, unicode: null),
                     Fileheight = dbtable.Column<int>(type: "int", nullable: false, unicode: null),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_mediafileid", item => item.Id);
                     dbtable.ForeignKey("fk_application_mediafile_siteid", item => item.Siteid, "Applicationsites", "Id");
                 });
        }

        protected override void Down(MigrationBuilder migrationbuilder)
        {

            migrationbuilder.DropColumn("Applicationmediafiles", "Applicationmediafiles");
            migrationbuilder.DropColumn("Applicationmodulesettings", "Applicationmodulesettings");
            migrationbuilder.DropColumn("Applicationmodules", "Applicationmodules");
            migrationbuilder.DropColumn("Applicationmodulepackages", "Applicationmodulepackages");
            migrationbuilder.DropColumn("Applicationcontainercolumns", "Applicationcontainercolumns");
            migrationbuilder.DropColumn("Applicationcontainers", "Applicationcontainers");
            migrationbuilder.DropColumn("Applicationpages", "Applicationpages");
            migrationbuilder.DropColumn("Applicationtenants", "Applicationtenants");
            migrationbuilder.DropColumn("Applicationsites", "Applicationsites");
            migrationbuilder.DropColumn("Applicationusers", "Applicationusers");

        }
    }
}
