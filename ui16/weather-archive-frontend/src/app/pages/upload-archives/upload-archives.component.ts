import { Component, ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-upload-archives',
  templateUrl: './upload-archives.component.html',
  styleUrls: ['./upload-archives.component.scss']
})
export class UploadArchivesComponent {
  selectedFiles: File[] = [];

  // Получаем доступ к input-элементу через ViewChild, чтобы программно его открывать
  @ViewChild('fileInput') fileInput!: ElementRef<HTMLInputElement>;

  // Вызывается при выборе файлов
  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (!input.files) {
      return;
    }

    const files: FileList = input.files;
    // Добавляем выбранные файлы в массив (без очистки, можно доработать проверку на дубликаты)
    for (let i = 0; i < files.length; i++) {
      this.selectedFiles.push(files.item(i)!);
    }
    // Очищаем value, чтобы можно было выбрать те же файлы ещё раз, если нужно
    input.value = '';
  }

  // Удаление файла из списка
  removeFile(index: number): void {
    this.selectedFiles.splice(index, 1);
  }

  // Пока без реальной отправки: выводим файлы в консоль
  submitFiles(): void {
    console.log('Файлы для отправки:', this.selectedFiles);
  }

  // Метод для программного открытия диалога выбора файлов
  openFileDialog(): void {
    this.fileInput.nativeElement.click();
  }
}
