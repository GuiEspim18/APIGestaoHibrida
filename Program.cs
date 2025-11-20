using HybridWork.Config;
using HybridWork.Repositories;
using HybridWork.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Config Bind
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

// Register Mongo client
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var config = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<MongoDBSettings>>().Value;
    return new MongoClient(config.ConnectionURI);
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
