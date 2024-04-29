using InDuckTor.Shared.Security.Jwt;
using InDuckTor.Shared.Strategies;
using InDuckTor.User.Features.Client.CreateClient;
using InDuckTor.User.Infrastructure.Database;
using InDuckTor.User.WebApi.Endpoints;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using InDuckTor.User.Features;
using InDuckTor.User.WebApi.Middlewares;
using FluentValidation;
using InDuckTor.Shared.Security.Http;
using InDuckTor.Shared.Configuration.Swagger;
using InDuckTor.User.Features.HttpClients;
using InDuckTor.User.WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddStrategiesFrom(Assembly.GetAssembly(typeof(ICreateClient))!);

// MediatR
foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
{
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
}

// Mapster
builder.Services.RegisterMapsterConfiguration();

// FluentValidation
builder.Services.RegisterValidatorConfiguration();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(BanUserValidator));

// EnumConverter
builder.Services.AddProblemDetails()
    .ConfigureJsonConverters();

// Authorization
builder.Services.AddInDuckTorAuthentication(builder.Configuration.GetSection(nameof(JwtSettings)));
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("roles", "employee"));
});
builder.Services.AddInDuckTorSecurity();

// Database
builder.Services.AddUsersDbContext(configuration);

// HTTP client
builder.Services.AddHttpClients(builder.Configuration);

// Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.ConfigureJwtAuth();
    options.ConfigureEnumMemberValues();
    options.UseOneOfForPolymorphism();

    var dir = new DirectoryInfo(AppContext.BaseDirectory);
        foreach (var fi in dir.EnumerateFiles("*.xml"))
        {
            var doc = XDocument.Load(fi.FullName);
            options.IncludeXmlComments(() => new XPathDocument(doc.CreateReader()), true);
        };
});
builder.Services.AddFluentValidationRulesToSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseExceptionHandler(new ExceptionHandlerOptions
{
    ExceptionHandler = new ExceptionMiddleware().Invoke
}
  );

app.UseAuthentication();
app.UseAuthorization();
//app.UseInDuckTorSecurity();

// Endpoints
app.AddClientEndpoints()
   .AddEmployeeEndpoints()
   .AddBlackListEndpoints()
   .AddUserEndpoints();

app.Run();
