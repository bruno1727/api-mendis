using ApiMendis;
using ApiMendis.Controllers.Requests;
using ApiMendis.Extensions;
using ApiMendis.Notifications;
using ApiMendis.OpenAI;
using ApiMendis.Services;
using ApiMendis.User;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                ;
        });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddSingleton(new JsonSerializerOptions(JsonSerializerDefaults.Web)
{
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    WriteIndented = true,
    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Latin1Supplement)
});

var config = builder.Configuration;



builder.Services.AddCache(config);

builder.Services.AddScoped<ITravelService, TravelService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IChatCompletionService, ChatCompletionService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

var travels = app.MapGroup("/travel");

travels.MapPost("/", async (GetTravelRequest request, ITravelService service) =>
{
    var result = await service.GetAsync(request);
    return result.Destinos;
});

var cache = app.MapGroup("/cache");

cache.MapPost("/flush",  (ICacheService service) =>
{
    service.FlushAll();
});

app.Run();
