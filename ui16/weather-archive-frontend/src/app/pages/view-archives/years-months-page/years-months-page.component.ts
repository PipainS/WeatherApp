import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { WeatherService } from 'src/app/shared/services/weather.service';
import { BaseComponent } from 'src/app/base/base.component';
import { ModalService } from 'src/app/shared/services/modal.service';
import { WeatherDateModel } from 'src/app/shared/interfaces/weather-date.model';
import { PaginationState } from 'src/app/shared/interfaces/pagination-state.model';
import { DEFAULT_PAGINATION } from 'src/app/shared/constants/pagination-defaults';
import { YearData } from 'src/app/shared/interfaces/year-data.model';

@Component({
  selector: 'app-years-months-page',
  templateUrl: './years-months-page.component.html',
  styleUrls: ['./years-months-page.component.scss']
})
export class YearsMonthsPageComponent extends BaseComponent implements OnInit {
  years: YearData[] = [];
  pagination: PaginationState = { ...DEFAULT_PAGINATION };

  constructor(
    private weatherService: WeatherService,
    protected override modalService: ModalService
  ) {
    super(modalService);
  }

  ngOnInit(): void {
    this.loadAvailableDates();
  }

  private loadAvailableDates(): void {
    var t = this;

    t.setLoading(true);
    t.weatherService.getAvailableDates()
      .pipe(finalize(() => t.setLoading(false)))
      .subscribe({
        next: (data: WeatherDateModel[]) => {
          t.years = data.map(d => ({
            year: d.year,
            expanded: false,
            months: d.months
          }));
        },
        error: (err) => {
          t.showResponseError(err);
        }
      });
  }

  toggleYear(year: YearData): void {
    year.expanded = !year.expanded;
  }

  changePage(page: number): void {
    this.pagination.currentPage = page;
  }
}
