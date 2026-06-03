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

  isLoading = false;

  constructor(
    private flightStatusService: FlightStatusService,
    private cdr: ChangeDetectorRef
  ) {
  }

  search(): void {

    if (!this.flightNumber.trim()) {

      this.errorMessage =
        'Flight number is required.';

      return;
    }

    if (!this.date) {

      this.errorMessage =
        'Flight date is required.';

      return;
    }

    this.errorMessage = '';
    this.result = undefined;
    this.isLoading = true;

    this.flightStatusService
      .getFlightStatus(
        this.flightNumber,
        this.date)
      .subscribe({
        next: (response) => {

          this.result = response;

          this.cdr.detectChanges();

          this.isLoading = false;
        },
        error: () => {

          this.errorMessage =
            'Unable to retrieve flight status.';

          this.isLoading = false;
        }
      });
  }

  getStatusText(status: number): string {

    switch (status) {

      case 0:
        return 'On Time';

      case 1:
        return 'Delayed';

      case 2:
        return 'Cancelled';

      case 3:
        return 'Diverted';

      default:
        return 'Unknown';
    }
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