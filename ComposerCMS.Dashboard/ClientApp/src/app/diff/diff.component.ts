import { Component, OnInit } from '@angular/core';
import { PageService } from '../services/page.service';
import { PageVersionService } from '../services/page-version.service';
import { ActivatedRoute } from '@angular/router';
import { forkJoin } from 'rxjs';
import { PageVersion } from '../../models/PageVersion';
import { Page } from '../../models/Page';

import SureToastManager from '../../common/toast';

@Component({
  selector: 'app-diff',
  templateUrl: './diff.component.html',
  providers: [PageService, PageVersionService]
})
export class DiffComponent implements OnInit {
  pageId: string;
  pageVersionId: string;

  page: Page;
  pageVersion: PageVersion;

  original: string =
  `
  this is the original string line 1
  here is line number 2
  and look we also have line 3
  `;

  modified: string =
  `
  this is the modified original string line 1
  here is line number 2

  additional line
  `;

  previousOutput: Array<any> = [];
  currentOutput: Array<any> = [];

  constructor(private _activatedRoute: ActivatedRoute, private _pageService: PageService, private _pageVersionService: PageVersionService) {
    this._activatedRoute.params.subscribe((params) => {
      this.pageId = params['pageId'];
      this.pageVersionId = params['pageVersionId'];

      const pageRequest = this._pageService.getByID(this.pageId);
      const pageVersionRequest = this._pageVersionService.getById(this.pageVersionId);

      forkJoin(pageRequest, pageVersionRequest).subscribe((response) => {
        this.page = response[0];
        this.pageVersion = response[1];

        this.configureDiff(this.pageVersion.content, this.page.content)
      });
    });
  }

  ngOnInit() {
    let toast = SureToastManager();
    toast.showSuccess('test', {});
  }

  configureDiff(previousContent, currentContent) {
    let previousLines = previousContent.split('\n');
    let currentLines = currentContent.split('\n');

    previousLines.forEach((line, originalIndex) => {
      if (originalIndex < currentLines.length - 1) {
        let modifiedLineAtOriginalIndex = currentLines[originalIndex];

        if (modifiedLineAtOriginalIndex != line) {
          this.previousOutput.push({
            text: line,
            color: '#FFEEF0'
          });

          this.currentOutput.push({
            text: modifiedLineAtOriginalIndex,
            color: '#E6FFED'
          });
        }
        else {
          this.previousOutput.push({
            text: line,
            color: ''
          });

          this.currentOutput.push({
            text: modifiedLineAtOriginalIndex,
            color: ''
          });
        }
      }
      else {
        this.previousOutput.push({
          text: line,
          color: '#FFEEF0'
        });
      }
    });

    // Any new lines at the end of the modified lines we need to push to the output.
    if (previousLines.length < currentLines.length) {
      for (var i = previousLines.length; i < currentLines.length; i++) {
        this.currentOutput.push({
          text: currentLines[i],
          color: '#E6FFED'
        });
      }
    }
  }
}
