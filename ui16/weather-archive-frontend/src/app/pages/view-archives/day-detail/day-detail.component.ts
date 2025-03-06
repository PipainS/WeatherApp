import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { WeatherService } from 'src/app/shared/services/weather.service';
import { WeatherData } from 'src/app/shared/interfaces/weather-data.model';
import { BaseComponent } from 'src/app/base/base.component';
import { ModalService } from 'src/app/shared/services/modal.service';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-day-detail',
  templateUrl: './day-detail.component.html',
  styleUrls: ['./day-detail.component.scss']
})
export class DayDetailComponent extends BaseComponent implements OnInit {
  private static readonly MONTH_OFFSET = 1;
  selectedDate!: Date;
  weatherData: WeatherData[] = [];


  constructor(
    private route: ActivatedRoute,
    private weatherService: WeatherService,
    protected override modalService: ModalService
  ) {
    super(modalService);
  }

  ngOnInit(): void {
    var t = this;

    t.selectedDate = t.getSelectedDate();
    t.loadWeatherData(t.selectedDate);
  }

  private getSelectedDate(): Date {
    const dateParam = this.route.snapshot.paramMap.get('date');
    return dateParam ? new Date(dateParam) : new Date();
  }

  private loadWeatherData(date: Date): void {
    var t = this;
    t.setLoading(true);

    const year = date.getFullYear();
    const month = date.getMonth() + DayDetailComponent.MONTH_OFFSET; 
    const day = date.getDate();

    t.weatherService.getWeatherByDay(year, month, day)
      .pipe(finalize(() => t.setLoading(false)))
      .subscribe({
        next: (data: WeatherData[]) => {
          t.weatherData = data;
        },
        error: (err) => {
          t.handleError(err);
        }
      });
  }

  private handleError(error: any): void {
    var t = this;

    if (error?.response) {
      t.showResponseError(error.response);
    } else {
      t.modalService.showModal('Ошибка', 'Произошла ошибка при загрузке данных. Пожалуйста, попробуйте позже.', 'ОК');
    }
  }
}
