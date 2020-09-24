import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PageService } from '../../services/page.service';
import { Page } from '../../models/Page';

@Component({
  selector: 'editor',
  templateUrl: './editor.component.html',
  providers: [PageService]
})
export class EditorComponent implements OnInit {
  pageId: string;
  page: Page = new Page();
  previewSrc: string = null;

  constructor(private _activatedRoute: ActivatedRoute, private _pageService: PageService) {
    this._activatedRoute.params.subscribe((params) => {
      this.pageId = params.pageId;
    })
  }

  ngOnInit() {
    this._pageService.getByID(this.pageId).subscribe((page) => {
      this.page = page;
      this.previewSrc = `http://localhost:51494/categories`;
    });
  }
}
