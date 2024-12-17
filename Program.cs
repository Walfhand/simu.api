using Hellang.Middleware.ProblemDetails;
using QuickApi.Engine.Web;
using Simu.Api.Configs;
using Simu.Api.Configs.Cors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddCustomCors();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseProblemDetails();
app.UseHttpsRedirection();
app.UseMinimalEndpoints();
app.UseCustomCors();

app.Run();