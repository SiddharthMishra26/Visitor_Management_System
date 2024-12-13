using Visitor_Management_System.Interface;
using Visitor_Management_System.Services;
using Visitor_Management_System.Common;
using Visitor_Management_System.Cosmos;
using Visitor_Management_System.ServiceFilters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IManagerService, ManagerService>();
builder.Services.AddSingleton<ISecurityService, SecurityService>();
builder.Services.AddSingleton<IVisitorService, VisitorService>();
//builder.Services.AddSingleton<IPassService, VisitorService>();
builder.Services.AddSingleton<IOfficeService, OfficeService>();
builder.Services.AddSingleton<IPassService, PassService>();
builder.Services.AddSingleton<ILoginService, LoginService>();
builder.Services.AddSingleton<ICosmosDbService, CosmosDbService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddSingleton<ExcelService>();


// Register the service filter
builder.Services.AddScoped<EnsurePassStatusFilter>();

// Add services to the container.
builder.Services.AddControllers();

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
