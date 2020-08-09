import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PageService } from '../../services/page.service';
import { Page } from '../../../models/Page';

@Component({
  selector: 'app-page-list',
  templateUrl: './page-list.component.html',
  providers: [PageService]
})
export class PageListComponent implements OnInit {
  pages: Page[] = [];
  systemPages: Page[] = [];

  constructor(private _router: Router, private _pageService: PageService) {

  }

  ngOnInit(): void {
    this._pageService.list().subscribe(pages => {
      console.log(pages);

      pages.forEach((page) => {
        if (page.isSystemPage) {
          this.systemPages.push(page);
        }
        else {
          console.log(page.isPublished);
          this.pages.push(page);
        }
      })
    });
  }
}
