using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RSIapi;
using RSIapi.Authorization;
using RSIapi.Context;
using RSIapi.Models;
using RSIapi.Services;
using System.Dynamic;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddSingleton<MyData>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var connection = String.Empty;
/*if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
}
else
{
    connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
}*/



//connection = "Server=tcp:fptodo-server.database.windows.net,1433;Initial Catalog=fptodo-database;Persist Security Info=False;User ID=fptodo-server-admin;Password=D00SKF01GC7I3I24$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

builder.Services.AddDbContext<ToDoItemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDB")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var authenicationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenicationSettings);
builder.Services.AddSingleton(authenicationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
    option.DefaultScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = authenicationSettings.JwtIssuer,
        ValidAudience = authenicationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenicationSettings.JwtKey)),
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("HasAge", builder => builder.RequireClaim("Age","33")); // ok ale jak mieć liczby? tablica wszyskich wieków xd
    options.AddPolicy("AtLeast20", builder => builder.AddRequirements(new MinimumAgeRequirement(20)));
});

builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeRequiremenHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<ContextSeeder>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options => 
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();
 
var myData = app.Services.GetRequiredService<MyData>();

myData.Info();


// use context singleton seeder to seed the database with roles
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<ContextSeeder>();
    seeder.Seed();
}



// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseCors(options => {
    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
