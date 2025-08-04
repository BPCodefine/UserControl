using Dapper;
using System.Data;

namespace UserControl
{
    public class Role
    {
        public int RoleId { get; set; }
        public required string RoleName { get; set; }
    }

    public static class RoleMan
    {
        public static void MapRoleEndpoints(this IEndpointRouteBuilder app)
        {
            //GET all roles
            app.MapGet("/roles", async (IDbConnection conn) =>
            {
                try
                {
                    var lines = await conn.QueryAsync<Role>("select * from [UserControl].dbo.[Roles]");
                    return Results.Ok(lines);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message, statusCode: 500);
                }
            });


            // POST: create a new role
            app.MapPost("/roles", async (Role role, IDbConnection db) =>
            {
                try
                {
                    var sql = "INSERT INTO [UserControl].dbo.Roles (RoleName) VALUES (@RoleName); SELECT SCOPE_IDENTITY();";
                    var id = await db.ExecuteScalarAsync<int>(sql, role);
                    role.RoleId = id;
                    return Results.Created($"/roles/{id}", role);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message, statusCode: 500);
                }
            });

            // PUT: update role name
            app.MapPut("/roles/{id}", async (int id, Role input, IDbConnection db) =>
            {
                try
                {
                    var sql = "UPDATE [UserControl].dbo.Roles SET RoleName = @RoleName WHERE RoleId = @RoleId";
                    var rows = await db.ExecuteAsync(sql, new { RoleId = id, input.RoleName });
                    return rows == 0 ? Results.NotFound() : Results.Ok(input);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message, statusCode: 500);
                }
            });

            // DELETE: remove role
            app.MapDelete("/roles/{id}", async (int id, IDbConnection db) =>
            {
                try
                {
                    var sql = "DELETE FROM [UserControl].dbo.Roles WHERE RoleId = @RoleId";
                    var rows = await db.ExecuteAsync(sql, new { RoleId = id });
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
