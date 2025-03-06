import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PreloaderComponent } from './shared/preloader/preloader.component';
import { HeaderComponent } from './layout/header/header.component';
import { ContentLayoutComponent } from './layout/header/content-layout/content-layout.component';
import { HomeComponent } from './pages/home/home.component';
import { UploadArchivesComponent } from './pages/upload-archives/upload-archives.component';
import { HttpClientModule } from '@angular/common/http';
import { YearsMonthsPageComponent } from './pages/view-archives/years-months-page/years-months-page.component';
import { FormsModule } from '@angular/forms';
import { DaysPageComponent } from './pages/view-archives/days-page/days-page.component';
import { DayDetailComponent } from './pages/view-archives/day-detail/day-detail.component';
import { registerLocaleData } from '@angular/common';
import localeRu from '@angular/common/locales/ru';


registerLocaleData(localeRu, 'ru');

@NgModule({
  declarations: [
    AppComponent,
    PreloaderComponent,
    HeaderComponent,
    ContentLayoutComponent,
    HomeComponent,
    YearsMonthsPageComponent,
    UploadArchivesComponent,
    DaysPageComponent,
    DayDetailComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'ru' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
