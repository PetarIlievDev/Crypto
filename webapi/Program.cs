using Infrastructure.RestClients;
using Infrastructure.RestClients.Interfaces;
using Serilog;
using WebApi.Services;
using WebApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.AddSerilog();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddTransient<ILogService, LogService>();
builder.Services.AddTransient<ICoinloreService, CoinloreService>();
builder.Services.AddHttpClient<ICoinloreClient, CoinloreClient>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
