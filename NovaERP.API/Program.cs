using ERP.API.Authorization;
using ERP.API.Configrations;
using ERP.Application;
using ERP.Application.Authorization;
using ERP.Core.Enums;
using ERP.Domain;
using ERP.Infreastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using SMS.API.Configurations;
using System.Security.Claims;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthenticationConfiguration(builder.Configuration);

builder.Services.AddAuthorization(options =>
    {
        foreach(PermissionModules module in Enum.GetValues<PermissionModules>())
        {
            foreach(PermissionAction action in Enum.GetValues<PermissionAction>())
            {
                var permission=new Permission(module, action);

                options.AddPolicy(permission.ToString(), policy =>
                {
                    policy.Requirements.Add(new PermissionRequirement(permission));
                });
            }
        }
    });

builder.Services.AddScoped<IAuthorizationHandler,PermissionHandler>();

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    // Policy: max 5 requests per 1 minute per IP
    options.AddPolicy("AuthLimiter", httpContext =>
    {
        var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: ip,
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            });
    });
});


builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

builder.Services.AddInfrastructure();
builder.Services.AddApplication();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfiguration();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ERPApiCorsPolicy", policy =>
    {
        policy
            .WithOrigins(
                "https://localhost:7280",
                "http://localhost:5007"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRateLimiter();

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
    {
        await context.Response.WriteAsync("Too many attempts. Please try again later.");
    }
});

app.UseCors("ERPApiCorsPolicy");

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous";
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var path = context.Request.Path.ToString();

        app.Logger.LogWarning(
            "Forbidden access. UserId={UserId}, Path={Path}, IP={IP}",
            userId,
            path,
            ip
        );
    }
});

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
