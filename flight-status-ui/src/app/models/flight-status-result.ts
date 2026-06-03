export interface FlightStatusResult {
  flightNumber: string;
  date: string;
  status: number;

  scheduledDepartureUtc: string;
  scheduledArrivalUtc: string;

  actualDepartureUtc?: string;
  actualArrivalUtc?: string;

  terminal?: string;
  gate?: string;
  delayReason?: string;

  lastUpdatedUtc: string;
  message: string;
}