using Diabetia.API;
using Diabetia.Application.UseCases;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Infrastructure.Providers;
using Diabetia.Infrastructure.Repositories;
using Infrastructure.Provider;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<diabetiaContext>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<RegisterUseCase>();
builder.Services.AddScoped<ConfirmUserEmailUseCase>();
builder.Services.AddScoped<ForgotPasswordUseCase>();
builder.Services.AddScoped<ConfirmForgotPasswordCodeUseCase>();
builder.Services.AddScoped<OcrDetectionUseCase>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IApiCognitoProvider, ApiCognitoProvider>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IApiAmazonService, ApiAmazonService>();


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
