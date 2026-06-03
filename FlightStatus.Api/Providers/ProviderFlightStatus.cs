using FlightStatus.Api.Models;

namespace FlightStatus.Api.Providers;

public class ProviderFlightStatus
{
    public string FlightNumber { get; set; } = string.Empty;

    public UnifiedFlightStatus Status { get; set; }

    public DateTime ScheduledDepartureUtc { get; set; }

    public DateTime ScheduledArrivalUtc { get; set; }

    public DateTime? ActualDepartureUtc { get; set; }

    public DateTime? ActualArrivalUtc { get; set; }

    public string? Terminal { get; set; }

    public string? Gate { get; set; }

    public string? DelayReason { get; set; }

    public DateTime LastUpdatedUtc { get; set; }

    public string ProviderName { get; set; } = string.Empty;
}