import { PreloaderComponent } from "../shared/preloader/preloader.component";

export abstract class BaseComponent {
  constructor() {

  }
  
  public setLoading(isLoading: boolean) {
    PreloaderComponent.setLoading(isLoading);
  }
}
