using FlightStatus.Api.Models;
using FlightStatus.Api.Providers;

namespace FlightStatus.Api.Services;

public class FlightStatusService : IFlightStatusService
{
    private readonly IEnumerable<IFlightStatusProvider> _providers;
    private readonly ILogger<FlightStatusService> _logger;

    public FlightStatusService(
        IEnumerable<IFlightStatusProvider> providers,
        ILogger<FlightStatusService> logger)
    {
        _providers = providers;
        _logger = logger;
    }

    public async Task<FlightStatusResult> GetStatusAsync(
        string flightNumber,
        DateTime date)
    {
        _logger.LogInformation(
            "Looking up flight {FlightNumber} for {Date}",
            flightNumber,
            date);

        var results = new List<ProviderFlightStatus>();

        foreach (var provider in _providers)
        {
            try
            {
                var response = await provider.GetStatusAsync(
                    flightNumber,
                    date);

                if (response != null)
                {
                    results.Add(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Provider failed while processing flight {FlightNumber}",
                    flightNumber);
            }
        }

        if (!results.Any())
        {
            _logger.LogWarning(
                "No provider returned data for flight {FlightNumber}",
                flightNumber);

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

        _logger.LogInformation(
            "Selected provider {ProviderName} for flight {FlightNumber}",
            selectedResult.ProviderName,
            flightNumber);

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