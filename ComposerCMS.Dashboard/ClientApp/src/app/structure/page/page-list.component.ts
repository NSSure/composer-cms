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
  pages: Page[] = new Array();

  constructor(private _router: Router, private _pageService: PageService) {

  }

  ngOnInit(): void {
    this._pageService.list().subscribe(pages => this.pages = pages);
  }
}
