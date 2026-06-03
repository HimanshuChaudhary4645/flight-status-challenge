using FlightStatus.Api.Models;

namespace FlightStatus.Api.Providers;

public class QuickFlightProvider : IFlightStatusProvider
{
    public Task<ProviderFlightStatus?> GetStatusAsync(
        string flightNumber,
        DateTime date)
    {
        var flights = new Dictionary<string, ProviderFlightStatus>
        {
            ["AI101"] = new()
            {
                FlightNumber = "AI101",
                Status = UnifiedFlightStatus.OnTime,
                ScheduledDepartureUtc = date.AddHours(10),
                ScheduledArrivalUtc = date.AddHours(12),
                LastUpdatedUtc = new DateTime(2026, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                ProviderName = "QuickFlight"
            },

            ["BA202"] = new()
            {
                FlightNumber = "BA202",
                Status = UnifiedFlightStatus.Delayed,
                ScheduledDepartureUtc = date.AddHours(9),
                ScheduledArrivalUtc = date.AddHours(11),
                LastUpdatedUtc = new DateTime(2026, 1, 1, 11, 0, 0, DateTimeKind.Utc),
                ProviderName = "QuickFlight"
            },

            ["EK505"] = new()
            {
                FlightNumber = "EK505",
                Status = UnifiedFlightStatus.Cancelled,
                ScheduledDepartureUtc = date.AddHours(14),
                ScheduledArrivalUtc = date.AddHours(18),
                LastUpdatedUtc = new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc),
                ProviderName = "QuickFlight"
            }
        };

        flights.TryGetValue(
            flightNumber.ToUpperInvariant(),
            out var result);

        return Task.FromResult(result);
    }
}