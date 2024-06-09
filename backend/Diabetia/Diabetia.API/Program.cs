using Amazon.CognitoIdentity.Model;
using Amazon.CognitoIdentityProvider;
using Diabetia.API.Controllers;
using Diabetia.Application.UseCases;
using Diabetia.Common.Utilities;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Infrastructure.EF;
using Diabetia.Infrastructure.Middlewares;
using Diabetia.Infrastructure.Providers;
using Diabetia.Infrastructure.Repositories;
using Diabetia.Interfaces;
using Infrastructure.Provider;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://localhost:3000",
        ValidAudience = "diabetia_users",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
    };
})
.AddCertificate();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<diabetiaContext>();
builder.Services.AddScoped<AuthLoginUseCase>();
builder.Services.AddScoped<AuthRegisterUseCase>();
builder.Services.AddScoped<AuthForgotPasswordUseCase>();
builder.Services.AddScoped<AuthChangePasswordUseCase>();
builder.Services.AddScoped<DataUserUseCase>();
builder.Services.AddScoped<TagDetectionUseCase>();
builder.Services.AddScoped<TagCalculateUseCase>();
builder.Services.AddScoped<EventPhysicalActivityUseCase>();
builder.Services.AddScoped<EventGlucoseUseCase>();
builder.Services.AddScoped<EventInsulinUseCase>();
builder.Services.AddScoped<AuthChangePasswordUseCase>();
builder.Services.AddScoped<HomeUseCase>();
builder.Services.AddScoped<CalendarUseCase>();

builder.Services.AddScoped<IEmailValidator, EmailValidator>();
builder.Services.AddScoped<IAuthProvider, AuthProvider>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITagRecognitionProvider, TagRecognitionProvider>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IHomeRepository, HomeRepository>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IInputValidator, InputValidator>();

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

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Mi API", Version = "v1" });

    // Configure el esquema de seguridad para JWT
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Ingrese el token JWT",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Agrega un requerimiento de seguridad para JWT
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
