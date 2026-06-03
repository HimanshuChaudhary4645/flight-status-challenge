# Flight Status Specification

## Assumptions

1. Providers are stubbed and deterministic.
2. Flight numbers are case-insensitive.
3. Latest provider timestamp wins.
4. Unknown is returned when no provider responds.

## Unified Status

- OnTime
- Delayed
- Cancelled
- Diverted
- Unknown

## Provider Contract

IFlightStatusProvider

Method:
GetStatusAsync(string flightNumber, DateTime date)

## FlightStatusResult

Fields:
- FlightNumber
- Date
- Status
- ScheduledDepartureUtc
- ScheduledArrivalUtc
- ActualDepartureUtc
- ActualArrivalUtc
- Terminal
- Gate
- DelayReason
- LastUpdatedUtc
- Message