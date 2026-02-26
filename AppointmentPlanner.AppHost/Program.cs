var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AppointmentPlanner>("Frontend");

builder.AddProject<Projects.AppointmentPlanner_Api>("appointmentplanner-api");

builder.Build().Run();
