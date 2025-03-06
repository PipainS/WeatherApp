import { PreloaderComponent } from "../shared/preloader/preloader.component";
import { ModalService } from "../shared/services/modal.service";

export abstract class BaseComponent {
  constructor(protected modalService: ModalService) {

  }
  
  public setLoading(isLoading: boolean) {
    PreloaderComponent.setLoading(isLoading);
  }

  protected showResponseError(response: any): void {
    if (response.errors && response.errors.length > 0) {
      const errorMessage = response.errors.map((error: { message: any; }) => error.message).join('\n');
      this.modalService.showModal('Ошибка', errorMessage, 'ОК');
    }
  }
}
