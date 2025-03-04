import { Component } from '@angular/core';

@Component({
  selector: 'app-preloader',
  templateUrl: './preloader.component.html',
  styleUrls: ['./preloader.component.scss'],
})
export class PreloaderComponent {
  constructor() {}

  public static setLoading(isLoading: boolean) {
    const preloader = document.getElementById('preloader');

    if (preloader) {
      preloader.style.display = isLoading ? 'flex' : 'none';
    }
  }
}
