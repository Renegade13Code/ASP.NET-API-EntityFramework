//This file is the entry point to the project
//First a builder(design patterns) is instantiated
using Microsoft.EntityFrameworkCore;
using webAPI.Data;
using webAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 
// Services are used to register dependencies, and perform dependecy injection within the project 
// Inject dependencies into the builder
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

//Add the regions/walks profile to services for automapping. It takes an assembly as its argument, and uses that assembly to scan all the profiles
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
