using OrderApi.Application.DependencyInjections;
using OrderApi.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.UseAuthorization();
app.UserInfrastructurePolicy();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
