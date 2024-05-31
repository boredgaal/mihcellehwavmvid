using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using Mihcelle.Hwavmvid.Server.Data;
using Mihcelle.Hwavmvid.Shared.Models;
using Mihcelle.Hwavmvid.Shared;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs
{

    [DbContext(typeof(Applicationdbcontext))]
    [Migration("mihcelle.hwavmvid.modules.chathubs.01.00.00.00")]
    public class Applicationmigration : Migration
    {

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            base.BuildTargetModel(modelBuilder);
        }

        protected override void Up(MigrationBuilder migrationbuilder)
        {


            migrationbuilder.CreateTable(
                 name: "ChatHubUser",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     FrameworkUserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     DisplayName = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     UserType = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubUser", item => item.Id);
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubRoom",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ModuleId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     CreatorId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Title = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Content = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     BackgroundColor = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ImageUrl = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     SnapshotUrl = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Type = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Status = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     OneVsOneId = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubRoom", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubRoom_Module", item => item.ModuleId, "Applicationmodules", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubRoomChatHubUser",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubRoomId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubUserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubRoomChatHubUser", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubRoomChatHubUser_ChatHubUser", item => item.ChatHubUserId, "ChatHubUser", "Id");
                     dbtable.ForeignKey("FK_ChatHubRoomChatHubUser_ChatHubRoom", item => item.ChatHubRoomId, "ChatHubRoom", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubMessage",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubRoomId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubUserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Content = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Type = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubMessage", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubMessage_ChatHubRoom", item => item.ChatHubRoomId, "ChatHubRoom", "Id");
                     dbtable.ForeignKey("FK_ChatHubMessage_User", item => item.ChatHubUserId, "ChatHubUser", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubConnection",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubUserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ConnectionId = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     IpAddress = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     UserAgent = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Status = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubConnection", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubConnection_ChatHubUser", item => item.ChatHubUserId, "ChatHubUser", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubPhoto",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubMessageId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Source = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Thumb = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Caption = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Size = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Width = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Height = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubPhoto", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubPhoto_ChatHubMessage", item => item.ChatHubMessageId, "ChatHubMessage", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubSetting",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubUserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     UsernameColor = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     MessageColor = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubSetting", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubSetting_User", item => item.ChatHubUserId, "ChatHubUser", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubDevice",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubUserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubRoomId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     UserAgent = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Type = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     DefaultDeviceId = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     DefaultDeviceName = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubDevice", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubDevice_ChatHubUser", item => item.ChatHubUserId, "ChatHubUser", "Id");
                     dbtable.ForeignKey("FK_ChatHubDevice_ChatHubRoom", item => item.ChatHubRoomId, "ChatHubRoom", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubInvitation",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubUserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     RoomId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Hostname = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubInvitation", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubInvitation_User", item => item.ChatHubUserId, "ChatHubUser", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubCam",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubRoomId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubConnectionId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Status = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     VideoUrl = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     VideoUrlExtension = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubCam", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubCam_ChatHubRoom", item => item.ChatHubRoomId, "ChatHubRoom", "Id");
                     dbtable.ForeignKey("FK_ChatHubCam_ChatHubConnection", item => item.ChatHubConnectionId, "ChatHubConnection", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubCamSequence",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubCamId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Filename = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     FilenameExtension = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubCamSequence", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubCamSequence_ChatHubCam", item => item.ChatHubCamId, "ChatHubCam", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubIgnore",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubUserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubIgnoredUserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubIgnore", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubIgnore_User", item => item.ChatHubUserId, "ChatHubUser", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubModerator",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubUserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ModeratorDisplayName = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubModerator", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubModerator_User", item => item.ChatHubUserId, "ChatHubUser", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubRoomChatHubModerator",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubRoomId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubModeratorId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubRoomChatHubModerator", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubRoomChatHubModerator_ChatHubRoom", item => item.ChatHubRoomId, "ChatHubRoom", "Id");
                     dbtable.ForeignKey("FK_ChatHubRoomChatHubModerator_ChatHubModerator", item => item.ChatHubModeratorId, "ChatHubModerator", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubWhitelistUser",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubUserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     WhitelistUserDisplayName = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubWhitelistUser", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubWhitelistUser_User", item => item.ChatHubUserId, "ChatHubUser", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubRoomChatHubWhitelistUser",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubRoomId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubWhitelistUserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubRoomChatHubWhitelistUser", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubRoomChatHubWhitelistUser_ChatHubRoom", item => item.ChatHubRoomId, "ChatHubRoom", "Id");
                     dbtable.ForeignKey("FK_ChatHubRoomChatHubWhitelistUser_ChatHubWhitelistUser", item => item.ChatHubWhitelistUserId, "ChatHubWhitelistUser", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubBlacklistUser",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubUserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     BlacklistUserDisplayName = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubBlacklistUser", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubBlacklistUser_User", item => item.ChatHubUserId, "ChatHubUser", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubRoomChatHubBlacklistUser",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubRoomId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubBlacklistUserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubRoomChatHubBlacklistUser", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubRoomChatHubBlacklistUser_ChatHubRoom", item => item.ChatHubRoomId, "ChatHubRoom", "Id");
                     dbtable.ForeignKey("FK_ChatHubRoomChatHubBlacklistUser_ChatHubBlacklistUser", item => item.ChatHubBlacklistUserId, "ChatHubBlacklistUser", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubGeolocation",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubConnectionId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     state = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     latitude = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     longitude = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     altitude = dbtable.Column<string>(type: "nvarchar(841)", nullable: true, unicode: null),
                     altitudeaccuracy = dbtable.Column<string>(type: "nvarchar(841)", nullable: true, unicode: null),
                     accuracy = dbtable.Column<string>(type: "nvarchar(841)", nullable: true, unicode: null),
                     heading = dbtable.Column<string>(type: "nvarchar(841)", nullable: true, unicode: null),
                     speed = dbtable.Column<string>(type: "nvarchar(841)", nullable: true, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubGeolocation", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubGeolocation_ChatHubConnection", item => item.ChatHubConnectionId, "ChatHubConnection", "Id");
                 });

        }

        protected override void Down(MigrationBuilder migrationbuilder)
        {

            migrationbuilder.DropColumn("ChatHubGeolocation", "ChatHubGeolocation");
            migrationbuilder.DropColumn("ChatHubRoomChatHubBlacklistUser", "ChatHubRoomChatHubBlacklistUser");
            migrationbuilder.DropColumn("ChatHubBlacklistUser", "ChatHubBlacklistUser");
            migrationbuilder.DropColumn("ChatHubRoomChatHubWhitelistUser", "ChatHubRoomChatHubWhitelistUser");
            migrationbuilder.DropColumn("ChatHubWhitelistUser", "ChatHubWhitelistUser");
            migrationbuilder.DropColumn("ChatHubRoomChatHubModerator", "ChatHubRoomChatHubModerator");
            migrationbuilder.DropColumn("ChatHubModerator", "ChatHubModerator");
            migrationbuilder.DropColumn("ChatHubIgnore", "ChatHubIgnore");
            migrationbuilder.DropColumn("ChatHubCamSequence", "ChatHubCamSequence");
            migrationbuilder.DropColumn("ChatHubCam", "ChatHubCam");
            migrationbuilder.DropColumn("ChatHubInvitation", "ChatHubInvitation");
            migrationbuilder.DropColumn("ChatHubDevice", "ChatHubDevice");
            migrationbuilder.DropColumn("ChatHubSetting", "ChatHubSetting");
            migrationbuilder.DropColumn("ChatHubPhoto", "ChatHubPhoto");
            migrationbuilder.DropColumn("ChatHubConnection", "ChatHubConnection");
            migrationbuilder.DropColumn("ChatHubMessage", "ChatHubMessage");
            migrationbuilder.DropColumn("ChatHubRoomChatHubUser", "ChatHubRoomChatHubUser");
            migrationbuilder.DropColumn("ChatHubUser", "ChatHubUser");
            migrationbuilder.DropColumn("ChatHubRoom", "ChatHubRoom");

        }
    }
}
