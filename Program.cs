using HybridWork.Config;
using HybridWork.Repositories;
using HybridWork.Services;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);


// Config Bind
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

// Register Mongo client
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var config = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<MongoDBSettings>>().Value;
    return new MongoClient(config.ConnectionURI);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});

// Register repository & service
builder.Services.AddSingleton<IWorkstationRepository, WorkstationRepository>();
builder.Services.AddTransient<WorkstationService>();

builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<EmployeeService>();

builder.Services.AddSingleton<IWorkspaceReservationRepository, WorkspaceReservationRepository>();
builder.Services.AddTransient<WorkspaceReservationService>();

builder.Services.AddSingleton<IHybridScheduleRepository, HybridScheduleRepository>();
builder.Services.AddTransient<HybridScheduleService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<JwtService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();
app.MapControllers();

app.Run();
