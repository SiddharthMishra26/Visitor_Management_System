using Visitor_Management_System.Interface;
using Visitor_Management_System.Services;
using Visitor_Management_System.Common;
using Visitor_Management_System.Cosmos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IManagerService, ManagerService>();
builder.Services.AddSingleton<ISecurityService, SecurityService>();
builder.Services.AddSingleton<IVisitorService, VisitorService>();
builder.Services.AddSingleton<IOfficeService, OfficeService>();
builder.Services.AddSingleton<IPassService, PassService>();
builder.Services.AddSingleton<ILoginService, LoginService>();
builder.Services.AddSingleton<ICosmosDbService, CosmosDbService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
