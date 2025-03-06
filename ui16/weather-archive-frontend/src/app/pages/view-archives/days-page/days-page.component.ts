import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DEFAULT_MONTH, DEFAULT_YEAR, ITEMS_PER_PAGE } from 'src/app/shared/constants/page-settings.constants';
import { DEFAULT_PAGINATION } from 'src/app/shared/constants/pagination-defaults';
import { PaginationState } from 'src/app/shared/interfaces/pagination-state.model';

@Component({
  selector: 'app-days-page',
  templateUrl: './days-page.component.html',
  styleUrls: ['./days-page.component.scss']
})
export class DaysPageComponent implements OnInit {
  days: Date[] = [];
  pagination: PaginationState = { ...DEFAULT_PAGINATION };
  selectedYear: number = DEFAULT_YEAR;
  selectedMonth: number = DEFAULT_MONTH;
  itemsPerPage: number = ITEMS_PER_PAGE;

  constructor(
    private route: ActivatedRoute, 
    private router: Router) {}

  ngOnInit(): void {
    var t = this;

    t.route.paramMap.subscribe(params => {
      t.selectedYear = Number(params.get('year'));
      t.selectedMonth = Number(params.get('month'));
      t.loadDays();
    });
  }

  loadDays(): void {
    var t = this;
  
    t.days = [];
    const daysInMonth = new Date(t.selectedYear, t.selectedMonth, 0).getDate();
  
    const allDays = [];
    for (let day = 1; day <= daysInMonth; day++) {
      allDays.push(new Date(t.selectedYear, t.selectedMonth - 1, day));
    }
  
    const startIndex = (t.pagination.currentPage - 1) * t.itemsPerPage;
    const endIndex = startIndex + t.itemsPerPage;
    t.days = allDays.slice(startIndex, endIndex);
  
    t.pagination.totalPages = Math.ceil(allDays.length / t.itemsPerPage);
  }
  
  changePage(page: number): void {
    if (page >= 1 && page <= this.pagination.totalPages) {
      this.pagination.currentPage = page;
      this.loadDays();
    }
  }

  goToDayDetail(day: Date): void {
    // Используем локальные компоненты даты вместо UTC
    const year = day.getFullYear();
    const month = (day.getMonth() + 1).toString().padStart(2, '0'); // Месяцы 0-11
    const date = day.getDate().toString().padStart(2, '0');
    
    this.router.navigate(['/day-detail', `${year}-${month}-${date}`]);
  }
}
