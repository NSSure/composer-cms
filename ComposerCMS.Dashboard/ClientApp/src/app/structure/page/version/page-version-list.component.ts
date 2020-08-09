import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PageVersionService } from '../../../services/page-version.service';
import { PageVersion } from '../../../../models/PageVersion';

@Component({
  selector: 'app-page-version-list',
  templateUrl: './page-version-list.component.html',
  providers: [PageVersionService]
})
export class PageVersionListComponent implements OnInit {
  pageId: string;
  pageVersions: PageVersion[] = new Array();

  constructor(private _activatedRoute: ActivatedRoute, private _pageVersionService: PageVersionService) {
    this._activatedRoute.params.subscribe(params => {
      this.pageId = params['id'];
    });
  }

  ngOnInit(): void {
    this._pageVersionService.list(this.pageId).subscribe(pageVersions => this.pageVersions = pageVersions);
  }
}
