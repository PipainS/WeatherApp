import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DataResponse } from '../interfaces/data-response.model';
import { environment } from 'src/environments/environment';

const baseUrl = `${environment.apiUrl}/api/WeatherArchive`

@Injectable({
  providedIn: 'root'
})
export class WeatherArchiveService {
  constructor(private http: HttpClient) { }

  uploadArchives(files: File[]): Observable<DataResponse<boolean>> {
    const formData = new FormData();
    files.forEach(file => {
      formData.append('archives', file, file.name);
    });

    return this.http.post<DataResponse<boolean>>(`${baseUrl}/uploadWeatherArchives`, formData);
  }
}
