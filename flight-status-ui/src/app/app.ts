import { Component, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { FlightStatusService } from './services/flight-status.service';
import { FlightStatusResult } from './models/flight-status-result';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {

  flightNumber = '';
  date = '';

  result?: FlightStatusResult;

  errorMessage = '';

  constructor(
    private flightStatusService: FlightStatusService,
     private cdr: ChangeDetectorRef
  ) {
  }

  search(): void {

    this.errorMessage = '';
    this.result = undefined;

    this.flightStatusService
      .getFlightStatus(
        this.flightNumber,
        this.date)
      .subscribe({
        next: (response) => {

          console.log('API Response', response);

          this.result = response;
          this.cdr.detectChanges();

          console.log('Result Assigned', this.result);

        },
        error: (err) => {

          console.error(err);

          this.errorMessage =
            'Unable to retrieve flight status.';
        }
      });
  }

  getType(value: any): string {
    return typeof value;
  }

  getStatusClass(status: number): string {

    switch (status) {

      case 0:
        return 'on-time';

      case 1:
        return 'delayed';

      case 2:
      case 3:
        return 'danger';

      default:
        return 'unknown';
    }
  }
}