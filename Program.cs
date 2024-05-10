using Microsoft.EntityFrameworkCore;
using Contexts;
using Services.Interfaces;
using Services;
using Services.External.Interfaces;
using Services.External;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransferService, TransferService>();
builder.Services.AddHttpClient<ITransferAuthorizer, TransferAuthorizer>(client =>
{
    client.BaseAddress = new Uri("https://run.mocky.io/v3/50baec25-ae9b-44c0-931f-8b1419992a9e");
});

builder.Services.AddDbContext<PostgreSqlContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
