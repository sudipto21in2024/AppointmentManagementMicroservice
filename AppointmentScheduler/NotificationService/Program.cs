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
using AppointmentService;
using NotificationService;
using NotificationService.Interfaces;
using NotificationService.Services;
using NotificationService.Settings;
using ServiceCatalog.Grpc;
using UserService;
using Grpc.Net.Client;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // Or any other logging provider

// gRPC Clients (Important: Add these)
builder.Services.AddGrpcClient<AppointmentServiceRPC.AppointmentServiceRPCClient>(options =>
{
    options.Address = new Uri(builder.Configuration["AppointmentService:GrpcUrl"]);
});
builder.Services.AddGrpcClient<ServiceGRPC.ServiceGRPCClient>(options =>
{
    options.Address = new Uri(builder.Configuration["ServiceCatalogService:GrpcUrl"]);
});
builder.Services.AddGrpcClient<UserServiceRPC.UserServiceRPCClient>(options =>
{
    options.Address = new Uri(builder.Configuration["UserService:GrpcUrl"]);
});


// Services (Dependency Injection)
builder.Services.AddHttpClient(); // For Service Catalog and User Service clients
builder.Services.AddScoped<IAppointmentService, AppointmentServiceClient>();
builder.Services.AddScoped<IServiceCatalogService, ServiceCatalogServiceClient>();
builder.Services.AddScoped<IUserService, UserServiceClient>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ITemplateProvider, TemplateProvider>();

//for cronos job scheduler
builder.Services.AddHostedService<DailyEmailScheduler>();
// ... other services if any

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings")); // Mail settings

// Add controllers if you have any API endpoints
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); // If you have controllers

app.Run();
