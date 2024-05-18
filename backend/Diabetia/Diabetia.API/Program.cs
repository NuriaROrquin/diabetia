using Diabetia.Application.UseCases;
using Diabetia.Domain.Services;
using Diabetia.Infrastructure.Providers;
using Infrastructure.Provider;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<RegisterUseCase>();
builder.Services.AddScoped<TagDetectionUseCase>();
builder.Services.AddScoped<TagCalculateUseCase>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IApiCognitoProvider, ApiCognitoProvider>();
builder.Services.AddScoped<IApiAmazonProvider, ApiAmazonProvider>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckles
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:3000")
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials();
});

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
