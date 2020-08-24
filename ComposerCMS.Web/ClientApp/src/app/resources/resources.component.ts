import { Component, ViewChild, OnInit } from '@angular/core';
import { ResourceListComponent } from './resource-list.component';
import { ExternalPackageService } from '../services/external-package.service';
import { PackageBundle } from '../../models/PackageBundle';

@Component({
  selector: 'app-resources',
  templateUrl: './resources.component.html',
  providers: [ExternalPackageService]
})
export class ResourcesComponent implements OnInit {
  @ViewChild(ResourceListComponent, { static: true }) resourceListComponent: ResourceListComponent;

  packageBundles: PackageBundle[] = [];

  constructor(private _externalPackageService: ExternalPackageService) {

  }

  ngOnInit() {
    this._externalPackageService.list().subscribe((packageBundles) => this.packageBundles = packageBundles);
  }

  queryExternalResources() {
    this.resourceListComponent.queryExternalResources();
  }
}
