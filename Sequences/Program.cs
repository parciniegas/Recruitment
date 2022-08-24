using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using Sequences.Data;
using Sequences.Data.Clients;
using Sequences.Services.Clients;
using Sequences.Services.Subjects;
using Sequences.Data.Subjects;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson();

var domain = builder.Configuration["Auth0:Domain"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = domain;
            options.Audience = builder.Configuration["Auth0:Audience"];
            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = ClaimTypes.NameIdentifier
            };
        });

builder.Services.AddApiVersioning(
        options =>
        {
            // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
            options.ReportApiVersions = true;

            // automatically applies an api version based on the name of the defining controller's namespace
            options.Conventions.Add(new VersionByNamespaceConvention());
        });

builder.Services.AddHttpLogging(options =>
        {
            options.LoggingFields = HttpLoggingFields.All;
            options.RequestBodyLogLimit = 4096;
            options.ResponseBodyLogLimit = 4096;
        });

builder.Services.AddHealthChecks()
    .AddDbContextCheck<Context>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Setup NLog as log provider
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();

builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        option => option.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorCodesToAdd: null))
    );

// Add application services
builder.Services.AddScoped<IClientsService, ClientServices>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ISubjectsService, SubjectsService>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpLogging();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();