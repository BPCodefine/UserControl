using Dapper;
using System.Data;

namespace UserControl
{

    public class Permission
    {
        public int PermissionId { get; set; }
        public int PageId { get; set; }
        public int RoleId { get; set; }
        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
    public static class PermissionMan
    {
        public static void MapPermissionEndpoints(this IEndpointRouteBuilder app)
        {
            //GET all Permission
            app.MapGet("/permissions", async (IDbConnection conn) =>
            {
                try
                {
                    var lines = await conn.QueryAsync<Permission>("select * from [UserControl].dbo.[Permissions]");
                    return Results.Ok(lines);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message, statusCode: 500);
                }
            });


            // POST: create a new role
            app.MapPost("/permissions", async (Permission perm, IDbConnection db) =>
            {
                try
                {
                    var sql = "INSERT INTO [UserControl].dbo.Permissions (PageID, RoleID, CanView, CanEdit, CanDelete) VALUES (@PageID, @RoleID, @CanView, @CanEdit, @CanDelete); SELECT SCOPE_IDENTITY();";
                    var id = await db.ExecuteScalarAsync<int>(sql, perm);
                    perm.PermissionId = id;
                    return Results.Created($"/Permission/{id}", perm);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message, statusCode: 500);
                }
            });

            // PUT: update role name
            app.MapPut("/permissions/{id}", async (int id, Permission perm, IDbConnection db) =>
            {
                try
                {
                    var sql = "UPDATE [UserControl].dbo.Permissions SET PageID = @PageID, RoleID = @RoleID, CanView = @CanView, CanEdit = @CanEdit, CanDelete = @CanDelete WHERE PermissionID = @PermissionID";
                    var rows = await db.ExecuteAsync(sql, new
                    {
                        PermissionID = id,
                        PageId = perm.PageId,
                        RoleId = perm.RoleId,
                        CanView = perm.CanView,
                        CanEdit = perm.CanEdit,
                        CanDelete = perm.CanDelete
                    });
                    return rows == 0 ? Results.NotFound() : Results.Ok(perm);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message, statusCode: 500);
                }
            });

            // DELETE: remove role
            app.MapDelete("/permissions/{id}", async (int id, IDbConnection db) =>
            {
                try
                {
                    var sql = "DELETE FROM [UserControl].dbo.Permissions WHERE PermissionID = @PermissionID";
                    var rows = await db.ExecuteAsync(sql, new { PermissionID = id });
                    return rows == 0 ? Results.NotFound() : Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message, statusCode: 500);
                }
            });
        }
    }
}
