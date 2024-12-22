using DevFreela.API.Filters;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Services.Implementations;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.Validators;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Configurations.MigrationConfig;
using DevFreela.Infrastructure.Persistence.Migrations;
using DevFreela.Infrastructure.Persistence.Repositories;
using FluentMigrator.Runner;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)));
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();


//CreateUserCommandValidator



builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevFreela.API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header usando o esquema Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string [] {}
        }
    });
});

builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(runner => runner
        .AddSqlServer() // Configura para SQL Server
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("Default")) // Usa a connection string configurada no appsettings.json
        .ScanIn(typeof(AddLoginColumns).Assembly).For.Migrations().For.EmbeddedResources())
    .AddLogging(lb => lb.AddFluentMigratorConsole()); // Habilita o log no console




var cns = builder.Configuration.GetConnectionString("DevFreelaDB");

Console.WriteLine(cns);
builder.Services.AddDbContext<DevFreelaDbContext>(p => p.UseSqlServer(cns)) ;


builder.Logging.ClearProviders(); // Remove provedores padrão
builder.Logging.AddConsole();     // Adiciona log no console
builder.Logging.AddDebug();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});


var app = builder.Build();


// Configure the HTTP request pipeline.

Console.WriteLine("passo pelo swagger");
    app.UseSwagger();
    app.UseSwaggerUI();

DatabaseManagementService.MigrationInitialisation(app);

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
