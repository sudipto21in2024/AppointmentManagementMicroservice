//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
using AppointmentService.Services;
using AS.Data.Repositories;
using CommonBase.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); // Load appsettings.json

// Logging
builder.Logging.ClearProviders(); // Clear default providers
builder.Logging.AddConsole();       // Add console logging (or other providers)

// Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Use SQL Server (or other DB)

// Repositories
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

// gRPC Services
builder.Services.AddGrpc(); // Add gRPC

// Your other services (if any)
// ...

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection(); // If using HTTPS

// gRPC Mapping
app.MapGrpcService<AppointmentGRPCService>(); // Map your gRPC service

app.Run();
