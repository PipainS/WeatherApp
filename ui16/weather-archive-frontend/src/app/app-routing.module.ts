import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContentLayoutComponent } from './layout/header/content-layout/content-layout.component';
import { HomeComponent } from './pages/home/home.component';
import { ViewArchivesComponent } from './pages/view-archives/view-archives.component';
import { UploadArchivesComponent } from './pages/upload-archives/upload-archives.component';

const routes: Routes = [
  {
    path: '',
    component: ContentLayoutComponent,
    children: [
      { path: 'home', component: HomeComponent },
      { path: 'view-archives', component: ViewArchivesComponent},
      { path: 'upload-archives', component: UploadArchivesComponent},
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
