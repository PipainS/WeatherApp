export interface WeatherData {
  weatherDateTime: Date;
  temperature: number;
  relativeHumidity: number;
  dewPoint: number;
  atmosphericPressure: number;
  windDirection?: string;
  windSpeed?: number;
  cloudiness?: number;
  cloudBaseHeight: number;
  visibility?: number;
  weatherPhenomena?: string;
}
