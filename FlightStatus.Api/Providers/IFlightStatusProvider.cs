namespace FlightStatus.Api.Providers;

public interface IFlightStatusProvider
{
    Task<ProviderFlightStatus?> GetStatusAsync(
        string flightNumber,
        DateTime date);
}