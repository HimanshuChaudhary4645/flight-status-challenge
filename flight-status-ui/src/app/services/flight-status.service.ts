import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FlightStatusResult } from '../models/flight-status-result';

@Injectable({
  providedIn: 'root'
})
export class FlightStatusService {

  private apiUrl =
    'http://localhost:5249/flights/status';

  constructor(
    private http: HttpClient) {
  }

  getFlightStatus(
    flightNumber: string,
    date: string
  ): Observable<FlightStatusResult> {

    return this.http.get<FlightStatusResult>(
      `${this.apiUrl}?flightNumber=${flightNumber}&date=${date}`);
  }
}