using Dapper;
using System;
using System.Data;

namespace UserControl
{
    public class User
    {
        public int UserId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
    public static class UserMan
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            //GET all users
            app.MapGet("/users", async (IDbConnection conn) =>
            {
                try
                {
                    var lines = await conn.QueryAsync<User>("select * from [UserControl].dbo.[Users]");
                    return Results.Ok(lines);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message, statusCode: 500);
                }
            });

            // POST a new user
            app.MapPost("/users", async (IDbConnection conn, User user) =>
            {
                try
                {
                    var id = await conn.ExecuteScalarAsync<int>("INSERT INTO [UserControl].dbo.[Users] (Email, Name) VALUES (@Email, @Name); SELECT SCOPE_IDENTITY();", user);
                    user.UserId = id;
                    return Results.Created($"/users/{id}", user);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message, statusCode: 500);
                }


            });

            // PUT: update a user
            app.MapPut("/users/{id}", async (IDbConnection conn, int id, User input) =>
            {
                try
                {
                    var rows = await conn.ExecuteAsync("UPDATE [UserControl].dbo.[Users] SET Email = @Email, Name = @Name WHERE UserId = @UserId", new
                    {
                        UserId = id,
                        Email = input.Email,
                        Name = input.Name
                    });
                    return rows == 0 ? Results.NotFound() : Results.Ok(input);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message, statusCode: 500);
                }
            });

            // DELETE: remove user
            app.MapDelete("/users/{id}", async (IDbConnection conn, int id) =>
            {
                try
                {
                    var rows = await conn.ExecuteAsync("DELETE FROM [UserControl].dbo.[Users] WHERE UserId = @UserId", new { UserId = id });
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