import { Component, ViewChild, OnInit, ElementRef } from '@angular/core';
import { ResourceListComponent } from './resource-list.component';
import { PackageBundle } from '../../models/PackageBundle';
import { HttpClient, HttpRequest, HttpEventType } from '@angular/common/http';
import { ExternalPackageService } from '../../services/external-package.service';

@Component({
  selector: 'app-resources',
  templateUrl: './resources.component.html',
  providers: [ExternalPackageService]
})
export class ResourcesComponent implements OnInit {
  @ViewChild(ResourceListComponent, { static: true }) resourceListComponent: ResourceListComponent;
  @ViewChild('fileInput', { static: true }) fileInput: ElementRef;

  packageName: string;
  packageBundles: PackageBundle[] = [];
  isAddingPackageBundle: boolean = false;

  constructor(private _externalPackageService: ExternalPackageService, private _http: HttpClient) {

  }

  ngOnInit() {
    this._externalPackageService.list().subscribe((packageBundles) => this.packageBundles = packageBundles);
  }

  queryExternalResources() {
    this.resourceListComponent.queryExternalResources();
  }

  uploadPackage(files) {
    this.upload(files, this.packageName);
  }

  upload(files: Array<File>, params: any) {
    if (files.length === 0) {
      return;
    }

    const formData = new FormData();

    for (const file of files) {
      formData.append(file.name, file);
    }

    formData.append('params', params);

    const uploadReq = new HttpRequest('POST', 'http://localhost:51494/api/external/package/upload', formData, {
      reportProgress: true,
    });

    this._http.request(uploadReq).subscribe(event => {
      switch (event.type) {
        case HttpEventType.UploadProgress:
          //let progress = Math.round(100 * event.loaded / event.total);
          break;
        case HttpEventType.Response:
          let message = event.body.toString();
          //let progress = null;
          this.fileInput.nativeElement.value = null;
          break;
      }
      if (event.type === HttpEventType.UploadProgress) {

      } else if (event.type === HttpEventType.Response) {

      }
    });
  }
}
