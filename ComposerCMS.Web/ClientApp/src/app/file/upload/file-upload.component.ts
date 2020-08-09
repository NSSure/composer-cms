import { Component, ViewChild, ElementRef, EventEmitter, Output } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html'
})
export class FileUploadComponent {
  @ViewChild('fileInput', { static: true }) fileInput: ElementRef;
  @Output() fileUploaded = new EventEmitter();

  progress: number;
  message: string;

  constructor(private _http: HttpClient) { }

  upload(files) {
    if (files.length === 0) {
      return;
    }

    const formData = new FormData();

    for (const file of files) {
      formData.append(file.name, file);
    }

    const uploadReq = new HttpRequest('POST', 'http://localhost:51494/api/file/upload', formData, {
      reportProgress: true,
    });

    this._http.request(uploadReq).subscribe(event => {
      if (event.type === HttpEventType.UploadProgress) {
        this.progress = Math.round(100 * event.loaded / event.total);
        console.log(this.progress);
      } else if (event.type === HttpEventType.Response) {
        this.message = event.body.toString();
        this.progress = null;
        this.fileInput.nativeElement.value = null;
        this.fileUploaded.emit();
      }
    });
  }
}
