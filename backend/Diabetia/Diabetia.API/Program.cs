using Amazon.CognitoIdentity.Model;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Diabetia.API;
using Diabetia.Application.UseCases;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Infrastructure.Providers;
using Diabetia.Infrastructure.Repositories;
using Infrastructure.Provider;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<diabetiaContext>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<RegisterUseCase>();
builder.Services.AddScoped<ForgotPasswordUseCase>();
builder.Services.AddScoped<DataUserUseCase>();
builder.Services.AddScoped<TagDetectionUseCase>();
builder.Services.AddScoped<TagCalculateUseCase>();
builder.Services.AddScoped<AddPhysicalEventUseCase>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthProvider, AuthProvider>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IApiAmazonProvider, ApiAmazonProvider>();
builder.Services.AddScoped<IEventRepository, EventRepository>();


builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();

var awsOptions = configuration.GetAWSOptions();

awsOptions.Region = Amazon.RegionEndpoint.USEast1;

awsOptions.Credentials = new Credentials()
{
    AccessKeyId = configuration["AWS_ACCESS_KEY_ID"],
    SecretKey = configuration["AWS_SECRET_ACCESS_KEY"],
};

builder.Services.AddDefaultAWSOptions(awsOptions);

builder.Services.AddAWSService<IAmazonCognitoIdentityProvider>();


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
