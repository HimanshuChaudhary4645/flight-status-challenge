using FlightStatus.Api.Models;

namespace FlightStatus.Api.Services;

public interface IFlightStatusService
{
    Task<FlightStatusResult> GetStatusAsync(
        string flightNumber,
        DateTime date);
}