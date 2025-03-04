import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PreloaderComponent } from './shared/preloader/preloader.component';
import { HeaderComponent } from './layout/header/header.component';
import { ContentLayoutComponent } from './layout/header/content-layout/content-layout.component';
import { HomeComponent } from './pages/home/home.component';
import { ViewArchivesComponent } from './pages/view-archives/view-archives.component';
import { UploadArchivesComponent } from './pages/upload-archives/upload-archives.component';

@NgModule({
  declarations: [
    AppComponent,
    PreloaderComponent,
    HeaderComponent,
    ContentLayoutComponent,
    HomeComponent,
    ViewArchivesComponent,
    UploadArchivesComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
