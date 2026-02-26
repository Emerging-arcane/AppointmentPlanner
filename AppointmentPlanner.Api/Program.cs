using AppointmentPlanner.Api.Data;
using AppointmentPlanner.Api.Endpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppointmentPlannerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppointmentPlanner")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Appointment Planner API v1");
    });

}

app.UseHttpsRedirection();

SaveAppointmentEndpoint.Map(app);
app.Run();


