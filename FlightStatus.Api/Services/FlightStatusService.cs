using FlightStatus.Api.Models;
using FlightStatus.Api.Providers;

namespace FlightStatus.Api.Services;

public class FlightStatusService : IFlightStatusService
{
    private readonly IEnumerable<IFlightStatusProvider> _providers;

    public FlightStatusService(
        IEnumerable<IFlightStatusProvider> providers)
    {
        _providers = providers;
    }

    public async Task<FlightStatusResult> GetStatusAsync(
        string flightNumber,
        DateTime date)
    {
        var results = new List<ProviderFlightStatus>();

        foreach (var provider in _providers)
        {
            var response = await provider.GetStatusAsync(
                flightNumber,
                date);

            if (response != null)
            {
                results.Add(response);
            }
        }

        if (!results.Any())
        {
            return new FlightStatusResult
            {
                FlightNumber = flightNumber,
                Date = date,
                Status = UnifiedFlightStatus.Unknown,
                Message = "No provider returned data."
            };
        }

        var selectedResult = results
            .OrderByDescending(x => x.LastUpdatedUtc)
            .First();

        return new FlightStatusResult
        {
            FlightNumber = selectedResult.FlightNumber,
            Date = date,
            Status = selectedResult.Status,
            ScheduledDepartureUtc = selectedResult.ScheduledDepartureUtc,
            ScheduledArrivalUtc = selectedResult.ScheduledArrivalUtc,
            ActualDepartureUtc = selectedResult.ActualDepartureUtc,
            ActualArrivalUtc = selectedResult.ActualArrivalUtc,
            Terminal = selectedResult.Terminal,
            Gate = selectedResult.Gate,
            DelayReason = selectedResult.DelayReason,
            LastUpdatedUtc = selectedResult.LastUpdatedUtc,
            Message = $"Data provided by {selectedResult.ProviderName}"
        };
    }
}