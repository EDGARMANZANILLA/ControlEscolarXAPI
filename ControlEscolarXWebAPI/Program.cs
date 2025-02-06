using Application;
using ControlEscolarXWebAPI.Middleware;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

//Define la politica de CORS
var corsAllowedOrigins = "_corsAllowedOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsAllowedOrigins,
                      policy =>
                      {
                          policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowAnyOrigin();
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/***/
builder.Services.AddServicesApplication();
builder.Services.AddServicesPersistence(builder.Configuration);

var app = builder.Build();

// Usa el middleware CORS
app.UseCors(corsAllowedOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//Use de un midleware como validacion de tipos
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
