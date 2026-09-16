using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hrms_mvc.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllProjects",
                columns: table => new
                {
                    ProjectsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectValue = table.Column<double>(type: "float", nullable: false),
                    PriceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllProjects", x => x.ProjectsId);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventModelId);
                });

            migrationBuilder.CreateTable(
                name: "EventsTypes",
                columns: table => new
                {
                    EventTypesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Colour = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsTypes", x => x.EventTypesId);
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DesignationFrom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DesignationTo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.PromotionId);
                    table.ForeignKey(
                        name: "FK_Promotions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resignations",
                columns: table => new
                {
                    ResignationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    NoticeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResignDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resignations", x => x.ResignationId);
                    table.ForeignKey(
                        name: "FK_Resignations_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resignations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentHead = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResignationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastDay = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resigns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Terminations",
                columns: table => new
                {
                    TerminationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TerminationType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NoticeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResignDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminations", x => x.TerminationId);
                    table.ForeignKey(
                        name: "FK_Terminations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RaisedBy = table.Column<int>(type: "int", nullable: false),
                    AssignedTo = table.Column<int>(type: "int", nullable: true),
                    AssignedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResolvedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_AssignedBy",
                        column: x => x.AssignedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Users_AssignedTo",
                        column: x => x.AssignedTo,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Users_RaisedBy",
                        column: x => x.RaisedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TasksId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TasksId);
                    table.ForeignKey(
                        name: "FK_Tasks_AllProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "AllProjects",
                        principalColumn: "ProjectsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketAttachments",
                columns: table => new
                {
                    AttachmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UploadedBy = table.Column<int>(type: "int", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketAttachments", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_TicketAttachments_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketAttachments_Users_UploadedBy",
                        column: x => x.UploadedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketComments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    CommentBy = table.Column<int>(type: "int", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketComments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_TicketComments_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketComments_Users_CommentBy",
                        column: x => x.CommentBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketResolutions",
                columns: table => new
                {
                    ResolutionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    ResolvedBy = table.Column<int>(type: "int", nullable: false),
                    Solution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResolutionNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResolvedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketResolutions", x => x.ResolutionId);
                    table.ForeignKey(
                        name: "FK_TicketResolutions_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketResolutions_Users_ResolvedBy",
                        column: x => x.ResolvedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaskBoards",
                columns: table => new
                {
                    TaskBoardsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TasksId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskBoards", x => x.TaskBoardsId);
                    table.ForeignKey(
                        name: "FK_TaskBoards_AllProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "AllProjects",
                        principalColumn: "ProjectsId");
                    table.ForeignKey(
                        name: "FK_TaskBoards_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "TasksId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskBoards_Tasks_TasksId",
                        column: x => x.TasksId,
                        principalTable: "Tasks",
                        principalColumn: "TasksId");
                });

            migrationBuilder.CreateTable(
                name: "Taskmembers",
                columns: table => new
                {
                    AssignedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taskmembers", x => x.AssignedId);
                    table.ForeignKey(
                        name: "FK_Taskmembers_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "TasksId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_UserId",
                table: "Promotions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Resignations_DepartmentId",
                table: "Resignations",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Resignations_UserId",
                table: "Resignations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskBoards_ProjectId",
                table: "TaskBoards",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskBoards_TaskId",
                table: "TaskBoards",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskBoards_TasksId",
                table: "TaskBoards",
                column: "TasksId");

            migrationBuilder.CreateIndex(
                name: "IX_Taskmembers_TaskId",
                table: "Taskmembers",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminations_UserId",
                table: "Terminations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAttachments_TicketId",
                table: "TicketAttachments",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAttachments_UploadedBy",
                table: "TicketAttachments",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComments_CommentBy",
                table: "TicketComments",
                column: "CommentBy");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComments_TicketId",
                table: "TicketComments",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketResolutions_ResolvedBy",
                table: "TicketResolutions",
                column: "ResolvedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TicketResolutions_TicketId",
                table: "TicketResolutions",
                column: "TicketId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssignedBy",
                table: "Tickets",
                column: "AssignedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssignedTo",
                table: "Tickets",
                column: "AssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_RaisedBy",
                table: "Tickets",
                column: "RaisedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "EventsTypes");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "Resignations");

            migrationBuilder.DropTable(
                name: "Resigns");

            migrationBuilder.DropTable(
                name: "TaskBoards");

            migrationBuilder.DropTable(
                name: "Taskmembers");

            migrationBuilder.DropTable(
                name: "Terminations");

            migrationBuilder.DropTable(
                name: "TicketAttachments");

            migrationBuilder.DropTable(
                name: "TicketComments");

            migrationBuilder.DropTable(
                name: "TicketResolutions");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "AllProjects");
        }
    }
}
