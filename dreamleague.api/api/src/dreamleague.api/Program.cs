using dreamleague.domain.Entities.Chat;
using dreamleague.domain.Entities.Players;
using dreamleague.shared.Configurations;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using dreamleague.hubs.Hubs;
using dreamleague.domain.Entities.Matches;
using dreamleague.api.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(c => c.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverterWithAttributeSupport()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SupportNonNullableReferenceTypes();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "dreamleague.api", Version = "v1" });

    var xmlFiles = Directory
        .GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly)
        .ToList();
    xmlFiles.ForEach(xmlFile => c.IncludeXmlComments(xmlFile));
});

// Inject application config
builder.Configuration.AddJsonFile("appsettings.json").AddEnvironmentVariables();
var applicationConfig = builder.Configuration.Get<ApplicationConfig>();
builder.Services.AddSingleton(applicationConfig);

// Injecting SignalR (microsoft's socket extension)
builder.Services.AddSignalR(o => { o.EnableDetailedErrors = true; });

// Inject dependencies
builder.Services.ConfigInfrastructures();
builder.Services.ConfigAdapters();
builder.Services.ConfigValidators();
builder.Services.ConfigServices();
builder.Services.ConfigHttpRepos(applicationConfig);

// Inject chat and queue tracking
builder.Services.AddSingleton<IDictionary<string, ChatInfo>>(opts => new Dictionary<string, ChatInfo>());
builder.Services.AddSingleton<IDictionary<string, Player>>(opts => new Dictionary<string, Player>());
builder.Services.AddSingleton<IDictionary<string, OngoingMatch>>(opts => new Dictionary<string, OngoingMatch>());

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy",
                      policy =>
                      {
                          policy.WithOrigins(applicationConfig.CorsConfig)
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                      });
});

// Correct controller routing
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Build web app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c => c.RouteTemplate = "api-docs/{documentName}/open-api.json");
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/api-docs/v1/open-api.json", "dreamleague.api v1");
        c.RoutePrefix = "api-docs";
        c.OAuthScopeSeparator(" ");
    });
}
app.UseRouting();
app.UseCors("CorsPolicy");
app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/api/chat");
    endpoints.MapHub<QueueHub>("/api/queue");
    endpoints.MapHub<NotificationHub>("/api/notification");
});
app.Run();
