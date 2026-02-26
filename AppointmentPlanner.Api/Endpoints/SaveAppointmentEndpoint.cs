using AppointmentPlanner.Api.Data;
using AppointmentPlanner.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AppointmentPlanner.Api.Endpoints
{
    public class SaveAppointmentEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/appointments", HandleAsync)
                .WithName("SaveAppointment")
                .WithTags("Appointments")
                .Produces<AppointmentResponse>(StatusCodes.Status201Created)
                .Produces<ValidationProblem>(StatusCodes.Status400BadRequest);
        }

        private static async Task<Results<Created<AppointmentResponse>, ValidationProblem>> HandleAsync(
            SaveAppointmentRequest request,
            AppointmentPlannerDbContext db,
            CancellationToken cancellationToken)
        {
            var connection = db.Database.GetDbConnection();
            
            if (connection.State != System.Data.ConnectionState.Open)
            {
                await connection.OpenAsync(cancellationToken);
            }

            using var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Appointments (Name, Email, CellNumber, AppointmentDate, AppointmentTime, HasBusiness)
                OUTPUT INSERTED.Id
                VALUES (@Name, @Email, @CellNumber, @AppointmentDate, @AppointmentTime, @HasBusiness)";

            command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@Name", request.Name));
            command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@Email", request.Email));
            command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@CellNumber", request.CellNumber));
            command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@AppointmentDate", request.AppointmentDate));
            command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@AppointmentTime", request.AppointmentTime));
            command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@HasBusiness", request.HasBusiness));

            var id = (int)await command.ExecuteScalarAsync(cancellationToken);

            var response = new AppointmentResponse(
                id,
                request.Name,
                request.Email,
                request.CellNumber,
                request.AppointmentDate,
                request.AppointmentTime,
                request.HasBusiness
            );

            return TypedResults.Created($"/api/appointments/{id}", response);
        }
    }

    public record SaveAppointmentRequest(
        string Name,
        string Email,
        string CellNumber,
        DateTime AppointmentDate,
        DateTime AppointmentTime,
        bool HasBusiness
    );

    public record AppointmentResponse(
        int Id,
        string Name,
        string Email,
        string CellNumber,
        DateTime AppointmentDate,
        DateTime AppointmentTime,
        bool HasBusiness
    );

    public interface IEndpoint
    {
        static abstract void Map(IEndpointRouteBuilder app);
    }
}
