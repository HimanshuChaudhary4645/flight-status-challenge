namespace FlightStatus.Api.Models;

public class FlightStatusResult
{
    public string FlightNumber { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public UnifiedFlightStatus Status { get; set; }

    public DateTime ScheduledDepartureUtc { get; set; }

    public DateTime ScheduledArrivalUtc { get; set; }

    public DateTime? ActualDepartureUtc { get; set; }

    public DateTime? ActualArrivalUtc { get; set; }

    public string? Terminal { get; set; }

    public string? Gate { get; set; }

    public string? DelayReason { get; set; }

    public DateTime LastUpdatedUtc { get; set; }

    public string Message { get; set; } = string.Empty;
}