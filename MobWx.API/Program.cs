var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var httpClientBaseUrl = builder.Configuration["HttpClient:WeatherApiAddress"] ?? "localhost";
var httpUserAgent = builder.Configuration["HttpClient:UserAgentHeader"] ?? null;
var httpAcceptHeader = builder.Configuration["HttpClient:AcceptHeader"] ?? null;
var httpCtsTimeout = builder.Configuration["HttpClient:CancelTokenTimeout"] ?? "2000";

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpClient("NwsClient", config =>
{
    config.BaseAddress = new Uri(httpClientBaseUrl);
    config.DefaultRequestHeaders.Add("Accept", httpAcceptHeader);
    config.DefaultRequestHeaders.Add("User-Agent", httpUserAgent);
    config.Timeout = TimeSpan.FromSeconds(int.Parse(httpCtsTimeout));
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/health", () =>
{
    return "Healthy";
})
.WithName("Health")
.WithOpenApi();

app.Run();
