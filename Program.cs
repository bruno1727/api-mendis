var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var travels = app.MapGroup("/travel");

var travelsList = new List<Travel>();

//travels.MapPost("/", (Travel travel) => { 
//    travelsList.Add(travel);
//    return Results.Created($"/travel/{travel.Id}",travel);
//});

travels.MapPost("/", createTravel);


travels.MapGet("/", () =>
{
    return travelsList;
});

travels.MapGet("/{id}", (int id) =>
{
    if (travelsList.Find(t => t.Id == id) == null) Results.NotFound();
    return travelsList.Find(t => t.Id == id);
});

travels.MapDelete("/{id}", (int id) =>
{
    if (travelsList.Find(t => t.Id == id) == null) Results.NotFound();
    return travelsList.RemoveAll(t => t.Id == id);
});

travels.MapPut("/{id}", (int id, Travel travel) =>
{
    if (travelsList.Find(t => t.Id == id) == null) Results.NotFound();
    travelsList.RemoveAll(t => t.Id == id);
    travelsList.Add(travel);
    return Results.NoContent();
});

async Task<IResult> createTravel(Travel travel)
{
    travelsList.Add(travel);
    return TypedResults.Created($"/travel/{travel.Id}", travel);
}

app.Run();

internal record Travel
{
    public int Id { get; set; }
    public Person? Person { get; set; }
    public Destination? Destination { get; set; }

}

internal record Person
{
    public string? FirstName { get; set; }
}

internal record Destination
{
    public string? Name { get; set; }
    public Country? Country { get; set; }

}

internal record Country
{
    public string? Name { get; set; }
    public Flag? Flag { get; set; }
}

internal record Flag
{
    public string? Url { get; set; }
}
