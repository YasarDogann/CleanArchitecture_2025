using CleanArhictecture_2025.Infrastructure;
using CleanArchitecture_2025.Application;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.OData;
using CleanArhictecture_2025.Application;
using CleanArchitecture_2025.WebAPI.Modules;
using CleanArhictecture_2025.WebAPI;
using CleanArhictecture_2025.WebAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCompression(opt =>
{
    opt.EnableForHttps = true;
});

builder.AddServiceDefaults();
//builder.Services.AddApplication();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCors();
builder.Services.AddOpenApi(); //open api swagger yerine kullanýcaz scalar ile
builder.Services
    .AddControllers()
    .AddOData(opt =>
    opt
    .Select()
    .Filter()
    .Count()
    .Expand()
    .OrderBy()
    .SetMaxTop(null)
    .AddRouteComponents("odata", AppODataController.GetEdmModel())
    );

builder.Services.AddRateLimiter(x =>
    x.AddFixedWindowLimiter("fixed", cfg =>
    {
        cfg.QueueLimit = 100;
        cfg.Window = TimeSpan.FromSeconds(1);
        cfg.PermitLimit = 100;
        cfg.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    }));

builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();


var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyHeader()
    .AllowCredentials()
    .AllowAnyMethod()
    .SetIsOriginAllowed(t => true));

app.RegisterRoutes();

app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCompression();

app.UseExceptionHandler();
app.MapControllers().RequireRateLimiting("fixed").RequireAuthorization();
ExtensionsMiddleware.CreateFirstUser(app);

app.Run();
