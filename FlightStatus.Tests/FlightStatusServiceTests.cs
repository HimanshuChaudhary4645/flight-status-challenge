using FlightStatus.Api.Models;
using FlightStatus.Api.Providers;
using FlightStatus.Api.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace FlightStatus.Tests;

public class FlightStatusServiceTests
{
    [Fact]
    public async Task Should_Return_Unknown_When_No_Provider_Returns_Data()
    {
        var providers = new List<IFlightStatusProvider>
        {
            new EmptyProvider()
        };

        var service = new FlightStatusService(
            providers,
            NullLogger<FlightStatusService>.Instance);

        var result = await service.GetStatusAsync(
            "TEST",
            DateTime.UtcNow);

        Assert.Equal(
            UnifiedFlightStatus.Unknown,
            result.Status);
    }

    [Fact]
    public async Task Should_Select_Result_With_Latest_Timestamp()
    {
        var providers = new List<IFlightStatusProvider>
        {
            new OlderProvider(),
            new NewerProvider()
        };

        var service = new FlightStatusService(
            providers,
            NullLogger<FlightStatusService>.Instance);

        var result = await service.GetStatusAsync(
            "AI101",
            DateTime.UtcNow);

        Assert.Equal(
            "Data provided by NewerProvider",
            result.Message);
    }
}

public class EmptyProvider : IFlightStatusProvider
{
    public Task<ProviderFlightStatus?> GetStatusAsync(
        string flightNumber,
        DateTime date)
    {
        return Task.FromResult<ProviderFlightStatus?>(null);
    }
}

public class OlderProvider : IFlightStatusProvider
{
    public Task<ProviderFlightStatus?> GetStatusAsync(
        string flightNumber,
        DateTime date)
    {
        return Task.FromResult<ProviderFlightStatus?>(
            new ProviderFlightStatus
            {
                FlightNumber = flightNumber,
                Status = UnifiedFlightStatus.OnTime,
                ScheduledDepartureUtc = date,
                ScheduledArrivalUtc = date.AddHours(2),
                LastUpdatedUtc = new DateTime(
                    2026, 1, 1, 10, 0, 0,
                    DateTimeKind.Utc),
                ProviderName = "OlderProvider"
            });
    }
}

public class NewerProvider : IFlightStatusProvider
{
    public Task<ProviderFlightStatus?> GetStatusAsync(
        string flightNumber,
        DateTime date)
    {
        return Task.FromResult<ProviderFlightStatus?>(
            new ProviderFlightStatus
            {
                FlightNumber = flightNumber,
                Status = UnifiedFlightStatus.Delayed,
                ScheduledDepartureUtc = date,
                ScheduledArrivalUtc = date.AddHours(2),
                LastUpdatedUtc = new DateTime(
                    2026, 1, 1, 12, 0, 0,
                    DateTimeKind.Utc),
                ProviderName = "NewerProvider"
            });
    }
}