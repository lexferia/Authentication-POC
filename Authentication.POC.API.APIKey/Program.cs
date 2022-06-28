using AspNetCore.Authentication.ApiKey;
using Authentication.POC.API.APIKey.Common;
using Microsoft.OpenApi.Models;

const string AuthKeyName = "X-API-KEY";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:8243")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "APIKey Authentication POC", Description = "Demo for sample implementation of APIKey authentication to be use in MEDU for BIZ API." });
    
    var apiKeyScheme = new OpenApiSecurityScheme
    {
        Description = "APIKey must appear in header",
        Type = SecuritySchemeType.ApiKey,
        Name = AuthKeyName,
        In = ParameterLocation.Header,
        Scheme = ApiKeyDefaults.AuthenticationScheme,
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = ApiKeyDefaults.AuthenticationScheme
        }
    };

    options.AddSecurityDefinition(apiKeyScheme.Reference.Id, apiKeyScheme);

    var requirement = new OpenApiSecurityRequirement
    {
        { apiKeyScheme, new List<string>() }
    };

    options.AddSecurityRequirement(requirement);
});

#region ASPNETCore.Authentication.APIKey

var authenticationSettings = builder.Configuration.GetSection("AuthenticationSettings").Get<AuthenticationSettings>();
builder.Services.AddSingleton(authenticationSettings);

builder.Services.AddAuthentication(ApiKeyDefaults.AuthenticationScheme)
                .AddApiKeyInHeader<APIKeyProvider>(options => 
                {
                    options.Realm = authenticationSettings.Realm;
                    options.KeyName = AuthKeyName;
                });

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
