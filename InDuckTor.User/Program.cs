using InDuckTor.Shared.Configuration;
using InDuckTor.Shared.Security;
using InDuckTor.Shared.Security.Jwt;
using InDuckTor.User.Infrastructure.Database;
using InDuckTor.User.WebApi.Endpoints;
using System.Xml.Linq;
using System.Xml.XPath;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddInDuckTorAuthentication(builder.Configuration.GetSection(nameof(JwtSettings)));
builder.Services.AddAuthorization();
builder.Services.AddInDuckTorSecurity();

builder.Services.AddUsersDbContext(configuration);

//builder.Services.AddProblemDetails()
//    .ConfigureJsonConverters();

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

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseInDuckTorSecurity();

app.AddClientEndpoints()
    .AddEmployeeEndpoints();

app.Run();
