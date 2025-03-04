import { Component, OnInit } from '@angular/core';

interface ArchiveItem {
  month?: string;
  year: number;
}

@Component({
  selector: 'app-view-archives',
  templateUrl: './view-archives.component.html',
  styleUrls: ['./view-archives.component.scss']
})
export class ViewArchivesComponent implements OnInit {
  activeTab: 'months' | 'years' = 'months';

  // Пример данных. В реальном приложении данные можно получать с сервиса.
  dataMonths: ArchiveItem[] = [
    { month: 'Январь', year: 2023 },
    { month: 'Февраль', year: 2023 },
    { month: 'Март', year: 2023 },
    // добавьте остальные месяцы или архивы
  ];

  dataYears: ArchiveItem[] = [
    { year: 2021 },
    { year: 2022 },
    { year: 2023 }
  ];

  // Пагинация
  currentPage: number = 1;
  itemsPerPage: number = 5;
  totalPages: number = 1;
  pagesArray: number[] = [];
  pagedData: ArchiveItem[] = [];

  ngOnInit(): void {
    this.loadData();
  }

  selectTab(tab: 'months' | 'years'): void {
    this.activeTab = tab;
    this.currentPage = 1;
    this.loadData();
  }

  loadData(): void {
    const data = this.activeTab === 'months' ? this.dataMonths : this.dataYears;
    this.totalPages = Math.ceil(data.length / this.itemsPerPage);
    this.pagesArray = Array.from({ length: this.totalPages }, (_, i) => i + 1);
    this.pagedData = data.slice(
      (this.currentPage - 1) * this.itemsPerPage,
      this.currentPage * this.itemsPerPage
    );
  }

  changePage(page: number): void {
    if (page < 1 || page > this.totalPages) {
      return;
    }
    this.currentPage = page;
    this.loadData();
  }
}
