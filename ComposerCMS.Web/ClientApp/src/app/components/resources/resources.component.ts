import { Component, ViewChild, OnInit, ElementRef, QueryList, ViewChildren } from '@angular/core';
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
  @ViewChildren(ResourceListComponent) resourceListComponents: QueryList<ResourceListComponent>;

  @ViewChild('fileInput', { static: true }) fileInput: ElementRef;
  @ViewChild('standaloneFiles', { static: true }) standaloneFiles: ElementRef;

  packageName: string;
  packageBundles: PackageBundle[] = [];
  isAddingPackageBundle: boolean = false;

  constructor(private _externalPackageService: ExternalPackageService, private _http: HttpClient) {

  }

  ngOnInit() {
    this._externalPackageService.list().subscribe((packageBundles) => this.packageBundles = packageBundles);
  }

  uploadPackage(files) {
    this.upload(files, 'http://localhost:51494/api/external/package/upload', this.packageName).subscribe(() => {
      this._externalPackageService.list().subscribe((packageBundles) => this.packageBundles = packageBundles);
    });
  }

  uploadStandaloneResources(files) {
    this.upload(files, 'http://localhost:51494/api/file/upload', null).subscribe(() => {
      this.resourceListComponents.last.queryExternalResources();
    });
  }

  upload(files: Array<File>, endpoint, params: any) {
    if (files.length === 0) {
      return;
    }

    const formData = new FormData();

    for (const file of files) {
      formData.append(file.name, file);
    }

    if (params) {
      formData.append('params', params);
    }

    const uploadReq = new HttpRequest('POST', endpoint, formData, {
      reportProgress: true,
    });

    return this._http.request(uploadReq);
  }
}
