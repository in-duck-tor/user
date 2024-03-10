using InDuckTor.Shared.Configuration;
using InDuckTor.Shared.Security;
using InDuckTor.Shared.Security.Jwt;
using InDuckTor.Shared.Strategies;
using InDuckTor.User.Features.Client.CreateClient;
using InDuckTor.User.Infrastructure.Database;
using InDuckTor.User.WebApi.Endpoints;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using System.Text.Json.Serialization;
using InDuckTor.User.Features;
using InDuckTor.User.WebApi.Middlewares;
using FluentValidation;

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
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Authorization
builder.Services.AddInDuckTorAuthentication(builder.Configuration.GetSection(nameof(JwtSettings)));
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("account_type", "service"));
});
builder.Services.AddInDuckTorSecurity();

// Database
builder.Services.AddUsersDbContext(configuration);   

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

app.UseHttpsRedirection();
app.UseCors();

app.UseExceptionHandler(new ExceptionHandlerOptions
{
    ExceptionHandler = new ExceptionMiddleware().Invoke
}
  );

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseInDuckTorSecurity();

// Endpoints
app.AddClientEndpoints()
   .AddEmployeeEndpoints()
   .AddBlackListEndpoints();

app.Run();
