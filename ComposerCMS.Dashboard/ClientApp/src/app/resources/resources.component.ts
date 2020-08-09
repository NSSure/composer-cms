import { Component, ViewChild } from '@angular/core';
import { ResourceListComponent } from './resource-list.component';

@Component({
  selector: 'app-resources',
  templateUrl: './resources.component.html'
})
export class ResourcesComponent {
  @ViewChild(ResourceListComponent, { static: true }) resourceListComponent: ResourceListComponent;

  queryExternalResources() {
    this.resourceListComponent.queryExternalResources();
  }
}
