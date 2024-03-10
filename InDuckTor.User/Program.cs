using InDuckTor.Shared.Configuration;
using InDuckTor.Shared.Security;
using InDuckTor.Shared.Security.Jwt;
using InDuckTor.Shared.Strategies;
using InDuckTor.User.Features.Client;
using InDuckTor.User.Features.Client.CreateClient;
using InDuckTor.User.Infrastructure.Database;
using InDuckTor.User.WebApi.Endpoints;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddStrategiesFrom(Assembly.GetAssembly(typeof(ICreateClient))!);

builder.Services.AddInDuckTorAuthentication(builder.Configuration.GetSection(nameof(JwtSettings)));
builder.Services.AddAuthorization();
builder.Services.AddInDuckTorSecurity();

builder.Services.AddUsersDbContext(configuration);

//builder.Services.AddProblemDetails()
//    .ConfigureJsonConverters();     

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();


app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseInDuckTorSecurity();

app.AddClientEndpoints()
    .AddEmployeeEndpoints();

app.Run();
