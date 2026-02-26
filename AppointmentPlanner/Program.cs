using AppointmentPlanner.Components;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;
using AppointmentPlanner.Data;
using Syncfusion.Licensing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<SfDialogService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddSyncfusionBlazor();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add controllers if you want to create API endpoints
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Enable Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Appointment Planner API v1");
    });
}

SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjGyl/VkR+XU9Ff1RBQmJAYVF2R2VJfl56d1VMY19BJAtUQF1hT35RdEViUHxac3xTQmFZWkd0");

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

// Map controllers if you added them
app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
