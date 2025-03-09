import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContentLayoutComponent } from './layout/header/content-layout/content-layout.component';
import { HomeComponent } from './pages/home/home.component';
import { UploadArchivesComponent } from './pages/upload-archives/upload-archives.component';
import { YearsMonthsPageComponent } from './pages/view-archives/years-months-page/years-months-page.component';
import { DaysPageComponent } from './pages/view-archives/days-page/days-page.component';
import { DayDetailComponent } from './pages/view-archives/day-detail/day-detail.component';

const routes: Routes = [
  {
    path: '',
    component: ContentLayoutComponent,
    children: [
      { path: 'home', component: HomeComponent },
      { path: 'view-archives', component: YearsMonthsPageComponent},
      { path: 'upload-archives', component: UploadArchivesComponent},
      { path: 'days/:year/:month', component: DaysPageComponent },
      { path: 'day-detail/:date', component: DayDetailComponent },
      { path: '', redirectTo: '/home', pathMatch: 'full' },
      { path: '**', redirectTo: '/home' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
