var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AppointmentPlanner>("Frontend");

builder.Build().Run();
