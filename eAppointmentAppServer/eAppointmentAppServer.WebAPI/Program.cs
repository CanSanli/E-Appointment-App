using DefaultCorsPolicyNugetPackage;
using eAppointmentAppServer.Application;
using eAppointmentAppServer.Infrastructure;
using eAppointmentAppServer.WebAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:SecretKey").Value ?? ""))
    };
});
builder.Services.AddAuthorizationBuilder();

builder.Services.AddDefaultCors(); //TS.defaultcorspolicy kütüphanesi. Enpoint olarak ayarlýyoruz

builder.Services.AddApplication();  //
builder.Services.AddInfrastructure(builder.Configuration);  //

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Configuration.AddEnvironmentVariables();
//Default => Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(setup =>      //bu sayede swaggerda token deðerimizi vererek giriþ yapabiliyoruz.
{
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
                });
}); ;

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

 //!

app.UseHttpsRedirection();

app.UseCors();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

CreateUserAsync.CreateUser(app).Wait();

app.Run();
