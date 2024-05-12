using Microsoft.EntityFrameworkCore;
using Contexts;
using Services.Interfaces;
using Services;
using Services.External.Interfaces;
using Services.External;
using Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransferService, TransferService>();
builder.Services.AddHttpClient<ITransferAuthorizer, TransferAuthorizer>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("TransferAuthorizerUri") ?? "");
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

app.UseErrorHandler();

app.Run();
