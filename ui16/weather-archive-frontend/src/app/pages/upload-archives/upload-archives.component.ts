import { Component, ViewChild, ElementRef } from '@angular/core';
import { BaseComponent } from 'src/app/base/base.component';
import { WeatherArchiveService } from 'src/app/shared/services/weather-archive.service';
import { DataResponse } from 'src/app/shared/interfaces/data-response.model';
import { firstValueFrom } from 'rxjs';
import { ModalService } from 'src/app/shared/services/modal.service';
import { ALLOWED_EXTENSIONS } from 'src/app/shared/constants/allowed-extensions.constants';

@Component({
  selector: 'app-upload-archives',
  templateUrl: './upload-archives.component.html',
  styleUrls: ['./upload-archives.component.scss']
})
export class UploadArchivesComponent extends BaseComponent {
  selectedFiles: File[] = [];

  @ViewChild('fileInput') fileInput!: ElementRef<HTMLInputElement>;

  constructor(
    private weatherArchiveService: WeatherArchiveService,
    protected override modalService: ModalService) {
    super(modalService);
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (!input.files) return;
    this.handleFileSelection(input.files);
    input.value = '';
  }

  handleFileSelection(files: FileList): void {
    Array.from(files).forEach(file => this.validateAndAddFile(file));
  }

  validateAndAddFile(file: File): void {
    var t = this;

    if (!t.isValidFile(file)) {
      t.modalService.showModal('Ошибка', `Файл ${file.name} имеет недопустимый формат. Разрешены только: ${ALLOWED_EXTENSIONS.join(', ')}`, 'ОК');
      return;
    }
    this.selectedFiles.push(file);
  }

  isValidFile(file: File): boolean {
    const fileExtension = file.name.split('.').pop()?.toLowerCase();
    return fileExtension ? ALLOWED_EXTENSIONS.includes(fileExtension) : false;
  }

  removeFile(index: number): void {
    this.selectedFiles.splice(index, 1);
  }

  async submitFiles(): Promise<void> {
    var t = this;

    if (t.selectedFiles.length === 0) {
      t.modalService.showModal('Ошибка', 'Выберите файлы для загрузки', 'ОК');
      return;
    }
  
    t.setLoading(true);
    try {
      const response = await firstValueFrom(t.weatherArchiveService.uploadArchives(t.selectedFiles));
      
      if (response) {
        t.handleUploadResponse(response);
      } else {
        t.modalService.showModal('Ошибка', 'Не удалось загрузить файлы', 'ОК');
      }
    } catch (error) {
      t.showResponseError({ errors: ['Ошибка при вызове сервиса: ' + error] });
    } finally {
      t.setLoading(false);
      t.selectedFiles = [];
    }
  }

  handleUploadResponse(response: DataResponse<boolean>): void {
    if (response.errors && response.errors.length > 0) {
      this.showResponseError(response);
    } else {
      this.modalService.showModal('Успех', 'Файлы успешно загружены', 'ОК');
    }
  }

  openFileDialog(): void {
    this.fileInput.nativeElement.click();
  }
}