using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace UserControl
{
    public class UserRoles
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
   
    public static class UserRolesMan
    {
        public static void MapUserRolesEndpoints(this IEndpointRouteBuilder app)
        {
            //GET all UserRoles
            app.MapGet("/userroles", async (IDbConnection conn) =>
            {
                try
                {
                    var lines = await conn.QueryAsync<UserRoles>("select * from [UserControl].dbo.[UserRoles]");
                    return Results.Ok(lines);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message, statusCode: 500);
                }
            });

            // POST a new UserRole
            app.MapPost("/userroles", async (IDbConnection conn, [AsParameters] UserRoles userRole) =>
            {
                try
                {
                    var id = await conn.ExecuteScalarAsync<int>("INSERT INTO [UserControl].dbo.[UserRoles] (UserID, RoleID) VALUES (@UserID, @RoleID); SELECT SCOPE_IDENTITY();", userRole);
                    return Results.Created($"/userroles?UserId={userRole.UserId}&RoleId={userRole.RoleId}", userRole);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message, statusCode: 500);
                }
            });

            // PUT edit UserRole
            app.MapPut("/userroles", async (IDbConnection conn, [AsParameters] UserRoles oldRole, [FromBody] UserRoles updated) => 
            {
                try
                {
                    await conn.ExecuteAsync("DELETE FROM [UserControl].dbo.[UserRoles] WHERE UserID = @UserId AND RoleID = @RoleId", new { UserId = oldRole.UserId, RoleId = oldRole.RoleId });
                    await conn.ExecuteAsync("INSERT INTO [UserControl].dbo.[UserRoles] (UserID, RoleID) VALUES (@UserId, @RoleId)", updated);
                    return Results.Ok(updated);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message, statusCode: 500);
                }

            });

            // DELETE: Remove a UserRole
            app.MapDelete("/userroles", async (IDbConnection conn, [AsParameters] UserRoles oldRole) =>
            {
                var rows = await conn.ExecuteAsync("DELETE FROM [UserControl].dbo.[UserRoles] WHERE UserID = @UserId AND RoleID = @RoleId", new { UserId = oldRole.UserId, RoleId = oldRole.RoleId });
                return rows > 0 ? Results.Ok() : Results.NotFound();
            });

        }
    }
}
