import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { DataResponse } from '../interfaces/data-response.model';
import { WeatherData } from '../interfaces/weather-data.model';
import { environment } from 'src/environments/environment';
import { WeatherDateModel } from '../interfaces/weather-date.model';
import { isValid } from '../utils/data-response.utils';

const baseUrl =  `${environment.apiUrl}/api/Weather`;

@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  constructor(private http: HttpClient) {}

  getAvailableDates(): Observable<WeatherDateModel[]> {
    return this.http.get<DataResponse<WeatherDateModel[]>>(`${baseUrl}/dates`)
      .pipe(
        map(response => {
          if (isValid(response) && response.data) {
            return response.data;
          } else {
            const errorMessage = response.errors.map(e => e.message).join(', ');
            throw new Error(errorMessage);
          }
        }),
        catchError(error => throwError(() => error))
      );
  }

  getWeatherByDay(year: number, month: number, day: number): Observable<WeatherData[]> {
    return this.http.get<DataResponse<WeatherData[]>>(`${baseUrl}/day/${year}/${month}/${day}`)
      .pipe(
        map(response => {
          if (isValid(response) && response.data) {
            return response.data;
          } else {
            const errorMessage = response.errors.map(e => e.message).join(', ');
            throw new Error(errorMessage);
          }
        }),
        catchError(error => throwError(() => error))
      );
  }  
}
