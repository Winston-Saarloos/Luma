using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luma.API.Migrations
{
    /// <inheritdoc />
    public partial class ForceSnakeCasingColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dashboard_layouts_dashboards_DashboardId",
                table: "dashboard_layouts");

            migrationBuilder.DropForeignKey(
                name: "FK_dashboards_users_UserId",
                table: "dashboards");

            migrationBuilder.DropForeignKey(
                name: "FK_widget_configs_widget_instances_WidgetInstanceId",
                table: "widget_configs");

            migrationBuilder.DropForeignKey(
                name: "FK_widget_instances_dashboards_DashboardId",
                table: "widget_instances");

            migrationBuilder.DropForeignKey(
                name: "FK_widget_instances_widgets_WidgetId",
                table: "widget_instances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_widgets",
                table: "widgets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_widget_instances",
                table: "widget_instances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_widget_configs",
                table: "widget_configs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dashboards",
                table: "dashboards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dashboard_layouts",
                table: "dashboard_layouts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "widgets",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TypeKey",
                table: "widgets",
                newName: "type_key");

            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "widgets",
                newName: "display_name");

            migrationBuilder.RenameIndex(
                name: "IX_widgets_TypeKey",
                table: "widgets",
                newName: "ix_widgets_type_key");

            migrationBuilder.RenameColumn(
                name: "Y",
                table: "widget_instances",
                newName: "y");

            migrationBuilder.RenameColumn(
                name: "X",
                table: "widget_instances",
                newName: "x");

            migrationBuilder.RenameColumn(
                name: "W",
                table: "widget_instances",
                newName: "w");

            migrationBuilder.RenameColumn(
                name: "H",
                table: "widget_instances",
                newName: "h");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "widget_instances",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "WidgetId",
                table: "widget_instances",
                newName: "widget_id");

            migrationBuilder.RenameColumn(
                name: "DashboardId",
                table: "widget_instances",
                newName: "dashboard_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "widget_instances",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_widget_instances_WidgetId",
                table: "widget_instances",
                newName: "ix_widget_instances_widget_id");

            migrationBuilder.RenameIndex(
                name: "IX_widget_instances_DashboardId",
                table: "widget_instances",
                newName: "ix_widget_instances_dashboard_id");

            migrationBuilder.RenameColumn(
                name: "Config",
                table: "widget_configs",
                newName: "config");

            migrationBuilder.RenameColumn(
                name: "WidgetInstanceId",
                table: "widget_configs",
                newName: "widget_instance_id");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "users",
                newName: "display_name");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "users",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_users_Email",
                table: "users",
                newName: "ix_users_email");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "dashboards",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "dashboards",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "dashboards",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "SortOrder",
                table: "dashboards",
                newName: "sort_order");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "dashboards",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_dashboards_UserId",
                table: "dashboards",
                newName: "ix_dashboards_user_id");

            migrationBuilder.RenameColumn(
                name: "Bp",
                table: "dashboard_layouts",
                newName: "bp");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "dashboard_layouts",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RowHeight",
                table: "dashboard_layouts",
                newName: "row_height");

            migrationBuilder.RenameColumn(
                name: "GridCols",
                table: "dashboard_layouts",
                newName: "grid_cols");

            migrationBuilder.RenameColumn(
                name: "DashboardId",
                table: "dashboard_layouts",
                newName: "dashboard_id");

            migrationBuilder.RenameIndex(
                name: "IX_dashboard_layouts_DashboardId_Bp",
                table: "dashboard_layouts",
                newName: "ix_dashboard_layouts_dashboard_id_bp");

            migrationBuilder.AddPrimaryKey(
                name: "pk_widgets",
                table: "widgets",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_widget_instances",
                table: "widget_instances",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_widget_configs",
                table: "widget_configs",
                column: "widget_instance_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_dashboards",
                table: "dashboards",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_dashboard_layouts",
                table: "dashboard_layouts",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_dashboard_layouts_dashboards_dashboard_id",
                table: "dashboard_layouts",
                column: "dashboard_id",
                principalTable: "dashboards",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_dashboards_users_user_id",
                table: "dashboards",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_widget_configs_widget_instances_widget_instance_id",
                table: "widget_configs",
                column: "widget_instance_id",
                principalTable: "widget_instances",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_widget_instances_dashboards_dashboard_id",
                table: "widget_instances",
                column: "dashboard_id",
                principalTable: "dashboards",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_widget_instances_widgets_widget_id",
                table: "widget_instances",
                column: "widget_id",
                principalTable: "widgets",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_dashboard_layouts_dashboards_dashboard_id",
                table: "dashboard_layouts");

            migrationBuilder.DropForeignKey(
                name: "fk_dashboards_users_user_id",
                table: "dashboards");

            migrationBuilder.DropForeignKey(
                name: "fk_widget_configs_widget_instances_widget_instance_id",
                table: "widget_configs");

            migrationBuilder.DropForeignKey(
                name: "fk_widget_instances_dashboards_dashboard_id",
                table: "widget_instances");

            migrationBuilder.DropForeignKey(
                name: "fk_widget_instances_widgets_widget_id",
                table: "widget_instances");

            migrationBuilder.DropPrimaryKey(
                name: "pk_widgets",
                table: "widgets");

            migrationBuilder.DropPrimaryKey(
                name: "pk_widget_instances",
                table: "widget_instances");

            migrationBuilder.DropPrimaryKey(
                name: "pk_widget_configs",
                table: "widget_configs");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_dashboards",
                table: "dashboards");

            migrationBuilder.DropPrimaryKey(
                name: "pk_dashboard_layouts",
                table: "dashboard_layouts");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "widgets",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "type_key",
                table: "widgets",
                newName: "TypeKey");

            migrationBuilder.RenameColumn(
                name: "display_name",
                table: "widgets",
                newName: "DisplayName");

            migrationBuilder.RenameIndex(
                name: "ix_widgets_type_key",
                table: "widgets",
                newName: "IX_widgets_TypeKey");

            migrationBuilder.RenameColumn(
                name: "y",
                table: "widget_instances",
                newName: "Y");

            migrationBuilder.RenameColumn(
                name: "x",
                table: "widget_instances",
                newName: "X");

            migrationBuilder.RenameColumn(
                name: "w",
                table: "widget_instances",
                newName: "W");

            migrationBuilder.RenameColumn(
                name: "h",
                table: "widget_instances",
                newName: "H");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "widget_instances",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "widget_id",
                table: "widget_instances",
                newName: "WidgetId");

            migrationBuilder.RenameColumn(
                name: "dashboard_id",
                table: "widget_instances",
                newName: "DashboardId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "widget_instances",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_widget_instances_widget_id",
                table: "widget_instances",
                newName: "IX_widget_instances_WidgetId");

            migrationBuilder.RenameIndex(
                name: "ix_widget_instances_dashboard_id",
                table: "widget_instances",
                newName: "IX_widget_instances_DashboardId");

            migrationBuilder.RenameColumn(
                name: "config",
                table: "widget_configs",
                newName: "Config");

            migrationBuilder.RenameColumn(
                name: "widget_instance_id",
                table: "widget_configs",
                newName: "WidgetInstanceId");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "display_name",
                table: "users",
                newName: "DisplayName");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "users",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_users_email",
                table: "users",
                newName: "IX_users_Email");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "dashboards",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "dashboards",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "dashboards",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "sort_order",
                table: "dashboards",
                newName: "SortOrder");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "dashboards",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_dashboards_user_id",
                table: "dashboards",
                newName: "IX_dashboards_UserId");

            migrationBuilder.RenameColumn(
                name: "bp",
                table: "dashboard_layouts",
                newName: "Bp");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "dashboard_layouts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "row_height",
                table: "dashboard_layouts",
                newName: "RowHeight");

            migrationBuilder.RenameColumn(
                name: "grid_cols",
                table: "dashboard_layouts",
                newName: "GridCols");

            migrationBuilder.RenameColumn(
                name: "dashboard_id",
                table: "dashboard_layouts",
                newName: "DashboardId");

            migrationBuilder.RenameIndex(
                name: "ix_dashboard_layouts_dashboard_id_bp",
                table: "dashboard_layouts",
                newName: "IX_dashboard_layouts_DashboardId_Bp");

            migrationBuilder.AddPrimaryKey(
                name: "PK_widgets",
                table: "widgets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_widget_instances",
                table: "widget_instances",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_widget_configs",
                table: "widget_configs",
                column: "WidgetInstanceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dashboards",
                table: "dashboards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dashboard_layouts",
                table: "dashboard_layouts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dashboard_layouts_dashboards_DashboardId",
                table: "dashboard_layouts",
                column: "DashboardId",
                principalTable: "dashboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dashboards_users_UserId",
                table: "dashboards",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_widget_configs_widget_instances_WidgetInstanceId",
                table: "widget_configs",
                column: "WidgetInstanceId",
                principalTable: "widget_instances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_widget_instances_dashboards_DashboardId",
                table: "widget_instances",
                column: "DashboardId",
                principalTable: "dashboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_widget_instances_widgets_WidgetId",
                table: "widget_instances",
                column: "WidgetId",
                principalTable: "widgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
