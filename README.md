# Flight Status Challenge

## Overview

This project implements a Flight Status lookup feature using ASP.NET Core Minimal API.

A support agent can search for a flight by flight number and date. The application queries multiple flight data providers, selects the most recent response, and returns a unified flight status model.

---

## Technologies Used

### Backend

- .NET 8
- ASP.NET Core Minimal API
- xUnit

### Frontend

- Angular
- TypeScript
- Angular HttpClient

---

## Solution Structure
```
FlightStatusChallenge
│
├── FlightStatus.Api
│   ASP.NET Core Minimal API
│
├── FlightStatus.Tests
│   Unit Tests
│
├── flight-status-ui
│   Angular Frontend
│
├── spec.md
├── prompts.md
├── reflection.md
└── README.md
```

---

## Assumptions

1. Providers are stubbed and deterministic.
2. Flight numbers are case-insensitive.
3. Dates are handled in UTC.
4. The latest provider response is selected using LastUpdatedUtc.
5. Unknown status is returned when no provider returns data.

---

## Architecture

### Components

* FlightStatus.Api
* FlightStatus.Tests
* flight-status-ui


### Providers

* AeroTrackProvider
* QuickFlightProvider

Both providers implement:

```csharp
IFlightStatusProvider
```

The service layer depends on abstractions rather than concrete implementations.

### Service Layer

```csharp
IFlightStatusService
FlightStatusService
```

Responsibilities:

* Query all providers
* Handle provider failures
* Select latest provider response
* Return unified flight status

---

## API Endpoint

### Get Flight Status

```http
GET /flights/status
```

Example:

```http
GET /flights/status?flightNumber=AI101&date=2026-06-03
```

---

## Running the Application

### Build

```bash
dotnet build
```

### Run

```bash
dotnet run --project FlightStatus.Api
```

### Swagger

Open:

```text
http://localhost:5249/swagger
```
---

## Frontend Application

The Angular frontend allows users to:

* Search by flight number and date
* View the latest flight status
* See provider information and flight details
* Receive validation and error feedback

### Run Frontend

```bash
cd flight-status-ui

npm install

ng serve
```

Open:

```text
http://localhost:4200
```

---

## Running Tests

```bash
dotnet test FlightStatus.Tests
```

---

## Logging

The application logs:

* Flight lookup requests
* Selected provider
* Provider failures
* Missing provider results

---

## Future Improvements

* Real provider integrations
* Caching
* Resilience policies
* Health checks
* Angular frontend enhancements
