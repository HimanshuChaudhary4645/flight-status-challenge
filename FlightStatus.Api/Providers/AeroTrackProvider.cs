using FlightStatus.Api.Models;

namespace FlightStatus.Api.Providers;

public class AeroTrackProvider : IFlightStatusProvider
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
                Status = UnifiedFlightStatus.Delayed,
                ScheduledDepartureUtc = date.AddHours(10),
                ScheduledArrivalUtc = date.AddHours(12),
                ActualDepartureUtc = date.AddHours(10).AddMinutes(45),
                Terminal = "T3",
                Gate = "A12",
                DelayReason = "Weather Conditions",
                LastUpdatedUtc = DateTime.UtcNow.AddMinutes(-10),
                ProviderName = "AeroTrack"
            },

            ["BA202"] = new()
            {
                FlightNumber = "BA202",
                Status = UnifiedFlightStatus.OnTime,
                ScheduledDepartureUtc = date.AddHours(9),
                ScheduledArrivalUtc = date.AddHours(11),
                LastUpdatedUtc = DateTime.UtcNow.AddMinutes(-15),
                ProviderName = "AeroTrack"
            },

            ["EK505"] = new()
            {
                FlightNumber = "EK505",
                Status = UnifiedFlightStatus.Cancelled,
                ScheduledDepartureUtc = date.AddHours(14),
                ScheduledArrivalUtc = date.AddHours(18),
                DelayReason = "Operational Issue",
                LastUpdatedUtc = DateTime.UtcNow.AddMinutes(-5),
                ProviderName = "AeroTrack"
            }
        };

        flights.TryGetValue(
            flightNumber.ToUpper(),
            out var result);

        return Task.FromResult(result);
    }
}