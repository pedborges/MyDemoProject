using Amazon;
using Amazon.CloudWatchLogs;
using Application;
using Application.Common;
using Application.UseCases;
using Domain.Contracts;
using Infrastructure;
using Infrastructure.DbClient;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.AwsCloudWatch;
using System;
using System.Text;
using TokenService.Service;

//we can also use SIGNALR withs hubs to cxomunicate streams data
//new implementations
//

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

#region Log Implementation

var logGroupName = "MyDemoProjectLogs";
var region = RegionEndpoint.USEast2;

//creating an Aws cloudWatch instance
var cloudWatchClient = new AmazonCloudWatchLogsClient(region);
var cloudWatchOptions = new CloudWatchSinkOptions
{
    LogGroupName = logGroupName,
    MinimumLogEventLevel = Serilog.Events.LogEventLevel.Information,
    CreateLogGroup = true,
    LogStreamNameProvider = new DefaultLogStreamProvider(),
    TextFormatter = new Serilog.Formatting.Json.JsonFormatter()
};

//here it will bind the logger serilog within cloudWatch
Log.Logger = new LoggerConfiguration()
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName()
    .Enrich.WithProcessId()
    .WriteTo.Console()
    .WriteTo.AmazonCloudWatch(cloudWatchOptions, cloudWatchClient)
    .CreateLogger();


builder.Host.UseSerilog();
#endregion
#region Dependencies Injection - Register layers
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
#endregion
#region Authentication & Authorization
var secretKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Jwt:Key is missing from configuration.");
var issuer = builder.Configuration["Jwt:Issuer"]
    ?? throw new InvalidOperationException("Jwt:Issuer is missing from configuration.");
var audience = builder.Configuration["Jwt:Audience"]
    ?? throw new InvalidOperationException("Jwt:Audience is missing from configuration.");
builder.Services.AddSingleton<IJWTService>(new JWTService(secretKey, 60));
builder.Services
  .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = issuer,
          ValidAudience = audience,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
          ClockSkew = TimeSpan.Zero
      };
  });
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.WebHost.UseUrls("http://*:8080");
#endregion
builder.Services.AddOpenApi();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{   
    app.MapOpenApi();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/openapi/v1.json", "MyDemoProject API v1");
    });
}
//app.UseHttpsRedirection();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
Task.Run(() =>
{
   using var scope = app.Services.CreateScope();
   var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
   initializer.Initialize();
});
Console.WriteLine("✅ Application started and listening on port 8080");
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.Run();

