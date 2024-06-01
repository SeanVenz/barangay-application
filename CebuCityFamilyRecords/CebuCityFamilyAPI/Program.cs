using CebuCityFamilyAPI.Context;
using CebuCityFamilyAPI.Repositories.AccountRepository;
using CebuCityFamilyAPI.Repositories.BarangayRepository;
using CebuCityFamilyAPI.Repositories.FamilyMembersRepository;
using CebuCityFamilyAPI.Repositories.FamilyRepository;
using CebuCityFamilyAPI.Services.AccountService;
using CebuCityFamilyAPI.Services.BarangayService;
using CebuCityFamilyAPI.Services.FamilyMembersService;
using CebuCityFamilyAPI.Services.FamilyService;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Add header documentation in swagger 
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Cebu City Barangay API",
        Description = "Barangay API for Cebu City",
        Contact = new OpenApiContact
        {
            Name = "Group 2 - Brgy Brent",
            Url = new Uri("https://github.com/CITUCCS/csit341-final-project-group-2-brgy-brent")
        },
    }); ;
    // Feed generated xml api docs to swagger
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add CORS middleware to allow requests from any origin
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    // Transcient
    services.AddTransient<DapperContext>();
    // Configure Automapper
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    // Services
    services.AddScoped<IBarangayService, BarangayService>();
    services.AddScoped<IFamilyMembersService, FamilyMembersService>();
    services.AddScoped<IFamilyService, FamilyService>();
    services.AddScoped<IAccountService, AccountService>();
    // Repository
    services.AddScoped<IBarangayRepository, BarangayRepository>();
    services.AddScoped<IFamilyRepository, FamilyRepository>();
    services.AddScoped<IFamilyMembersRepository, FamilyMembersRepository>();
    services.AddScoped<IAccountRepository, AccountRepository>();
}
