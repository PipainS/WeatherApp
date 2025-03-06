import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { MODAL_DEFAULTS } from '../constants/modal.constants';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss']
})
export class ModalComponent {
  @Input() title = MODAL_DEFAULTS.TITLE;
  @Input() message = MODAL_DEFAULTS.MESSAGE;
  @Input() buttonText = MODAL_DEFAULTS.BUTTON_TEXT;

  constructor(public activeModal: NgbActiveModal) {}

  closeModal(): void {
    this.activeModal.close();
  }
}
