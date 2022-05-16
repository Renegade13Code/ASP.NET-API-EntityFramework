//This file is the entry point to the project
//First a builder(design patterns) is instantiated
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using webAPI.Data;
using webAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 
// Services are used to register dependencies, and perform dependecy injection within the project 
// Inject dependencies into the builder
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
// Options added to ensure swagger uses recieved jwt token
builder.Services.AddSwaggerGen(options =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter a valid JWT bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme, new string[] {} }
    });
});

//This will add fluent validation
builder.Services
    .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Program>());

//Register the dbContext as a dependency for dependency injection
builder.Services.AddDbContext<NZWalksDbContext>(options =>
{
    // UseSqlServer takes the connection string to the database as its argument. Here the connection string is grabed from the appsettings.json file via the builder object.
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalks")); 
});

//This registers the RegionRepository class within services. It tells dotnet that whenever IRegionRepository is specified, it should instantiate and inject the RegionRepository class
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<IWalkRepository, WalkRepository>();
builder.Services.AddScoped<IWalkDifficultyRepository, WalkDifficultyRepository>();
builder.Services.AddScoped<IMyTokenHandler, MyTokenHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// This was used for the implementation of the staticUserRepo. A singleton is needed so a single instance is instantiated, else multiple instances will contain different Guid id's
//builder.Services.AddSingleton<IUserRepository, StaticUserRepository>();

//Add the regions/walks profile to services for automapping. It takes an assembly as its argument, and uses that assembly to scan all the profiles
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// This allows injection of authentication
// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/?view=aspnetcore-6.0
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Inserted before 'app.UseAuthorization()', this is done to ensure user is authenticated before checking if 
// user has authorization to access endpoint.
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
