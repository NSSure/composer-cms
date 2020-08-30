import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ExternalResource } from '../../models/ExternalResource';
import { ExternalResourceService } from '../../services/external-resource.service';

@Component({
  selector: 'app-resource-list',
  templateUrl: './resource-list.component.html',
  providers: [ExternalResourceService]
})
export class ResourceListComponent implements OnInit {
  @Input() showAction: boolean;
  @Input() actionText: string;
  @Input() method: any;
  @Input() manualQuery: boolean = false;
  @Input() externalResources: ExternalResource[];
  @Input() viewMode: string = 'grid';

  @Output() onActionClicked = new EventEmitter<ExternalResource>();

  constructor(private _externalResourceService: ExternalResourceService) { }

  ngOnInit(): void {
    this.queryExternalResources();
  }

  queryExternalResources() {
    if (!this.manualQuery) {
      this._externalResourceService.list().subscribe(externalResources => this.externalResources = externalResources);
    }
  }

  viewResource(externalResource: ExternalResource) {
    window.open(`http://localhost:51494${externalResource.path}`);
  }
}
