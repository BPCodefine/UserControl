using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace UserControl
{
    public class AppPage
    {
        public int PageId { get; set; }
        public required string PageName { get; set; }
        public required string AppName { get; set; }
        public required string Path { get; set; }
    }

    public static class AppPageMan
    {
        public static void MapAppPageEndpoints(this IEndpointRouteBuilder app)
        {
            //GET all AppPages
            app.MapGet("/apppages", async (IDbConnection conn) =>
            {
                try
                {
                    var lines = await conn.QueryAsync<AppPage>("select * from [UserControl].dbo.[AppPages]");
                    return Results.Ok(lines);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message, statusCode: 500);
                }
            });


            // POST: create a new AppPage
            app.MapPost("/apppages", async (AppPage AppPage, IDbConnection db) =>
            {
                try
                {
                    var sql = "INSERT INTO [UserControl].dbo.AppPages (PageName, AppName, Path) VALUES (@PageName, @AppName, @Path); SELECT SCOPE_IDENTITY();";
                    var id = await db.ExecuteScalarAsync<int>(sql, AppPage);
                    AppPage.PageId = id;
                    return Results.Created($"/AppPages/{id}", AppPage);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message, statusCode: 500);
                }
            });

            // PUT: update AppPage name
            app.MapPut("/apppages/{id}", async (int id, AppPage input, IDbConnection db) =>
            {
                try
                {
                    var sql = "UPDATE [UserControl].dbo.AppPages SET PageName = @PageName, AppName = @AppName, Path = @Path WHERE PageId = @PageId";
                    var rows = await db.ExecuteAsync(sql, new
                    {
                        PageId = id,
                        PageName = input.PageName,
                        AppName = input.AppName,
                        Path = input.Path
                    });
                    return rows == 0 ? Results.NotFound() : Results.Ok(input);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message, statusCode: 500);
                }
            });

            // DELETE: remove AppPage
            app.MapDelete("/apppages/{id}", async (int id, IDbConnection db) =>
            {
                try
                {
                    var sql = "DELETE FROM [UserControl].dbo.AppPages WHERE PageId = @PageId";
                    var rows = await db.ExecuteAsync(sql, new { PageId = id });
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
