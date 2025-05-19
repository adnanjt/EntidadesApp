//using Microsoft.OpenApi.Models;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();


using Microsoft.OpenApi.Models;
using System.Text;
using EntidadesApi.Application.Interfaces;
using EntidadesApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using EntidadesApi.Application.Services;
using EntidadesApi.Domain.Interfaces;
using EntidadesApi.Application.Mappers;
using AutoMapper;


var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.Host.UseSerilog((ctx, config) =>
    config.ReadFrom.Configuration(ctx.Configuration).WriteTo.Console());

// JWT Config (clave simple solo para pruebas)
var jwtKey = builder.Configuration["Jwt:Key"] ?? "ClaveSuperSecreta12345";
var key = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);



// Servicios
// Register repositories with file paths from configuration
var tipoEntidadFilePath = builder.Configuration.GetValue<string>("FileSettings:TipoEntidadFile") ?? "";
var sectorFilePath = builder.Configuration.GetValue<string>("FileSettings:SectorFile") ?? "";
var entidadGubernamentalFilePath = builder.Configuration.GetValue<string>("FileSettings:EntidadGubernamentalFile") ?? "";

// Register TipoEntidadRepository with file path
builder.Services.AddSingleton<ITipoEntidadRepository>(provider =>
    new TipoEntidadRepository(tipoEntidadFilePath));

// Register SectorRepository with file path
builder.Services.AddSingleton<ISectorRepository>(provider =>
    new SectorRepository(sectorFilePath));

// Register EntidadGubernamentalRepository with file path and dependencies
builder.Services.AddSingleton<IEntidadGubernamentalRepository>(provider =>
{
    var tipoEntidadRepository = provider.GetRequiredService<ITipoEntidadRepository>();
    var sectorRepository = provider.GetRequiredService<ISectorRepository>();
    return new EntidadGubernamentalRepository(
        entidadGubernamentalFilePath,
        tipoEntidadRepository,
        sectorRepository);
});

builder.Services.AddSingleton<IEntidadGubernamentalService, EntidadGubernamentalService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT en el encabezado usando el esquema Bearer. Ejemplo: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors(options =>
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));

var app = builder.Build();
app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
