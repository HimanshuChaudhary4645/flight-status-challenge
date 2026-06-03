using FlightStatus.Api.Providers;
using FlightStatus.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularClient",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddScoped<IFlightStatusProvider, AeroTrackProvider>();
builder.Services.AddScoped<IFlightStatusProvider, QuickFlightProvider>();

builder.Services.AddScoped<IFlightStatusService, FlightStatusService>();

var app = builder.Build();

app.UseCors("AngularClient");

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet(
    "/flights/status",
    async (
        string flightNumber,
        DateTime date,
        IFlightStatusService service) =>
    {
        if (string.IsNullOrWhiteSpace(flightNumber))
        {
            return Results.BadRequest("Flight number is required.");
        }

        var result = await service.GetStatusAsync(
            flightNumber,
            date);

        return Results.Ok(result);
    });

app.Run();