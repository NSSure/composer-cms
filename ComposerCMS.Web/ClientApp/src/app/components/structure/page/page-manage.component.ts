import { Component, OnInit, ViewChild, ViewChildren, QueryList } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import SureToastManager from '../../../../common/toast';
import { Page } from '../../../models/Page';
import { ExternalResource } from '../../../models/ExternalResource';
import { HtmlEditorComponent } from '../../html-editor/html-editor.component';
import { PageScript } from '../../../models/PageScript';
import { PageService } from '../../../services/page.service';
import { PageScriptService } from '../../../services/page-script.service';

@Component({
  selector: 'app-page-manage',
  templateUrl: './page-manage.component.html',
  providers: [PageService, PageScriptService]
})
export class PageManageComponent implements OnInit {
  @ViewChildren('htmlEditor') htmlEditor: QueryList<HtmlEditorComponent>;

  page: Page;
  pageId: string;
  scriptResources: ExternalResource[] = [];

  toast: any;

  constructor(private _activatedRotue: ActivatedRoute, private _pageService: PageService, private _pageScriptService: PageScriptService) {
    this.toast = SureToastManager();

    this._activatedRotue.params.subscribe(params => {
      if (params['id']) {
        this.pageId = params['id'];
        this._pageService.getByID(this.pageId).subscribe(page => this.handlePage(page));
      } else {
        this.page = new Page();
      }
    });
  }

  ngOnInit() {
    this._pageScriptService.listScriptResourcesByPage(this.pageId).subscribe((scriptResources) => {
      this.scriptResources = scriptResources
    });
  }

  handlePage(page: Page): void {
    this.page = page;
    console.log(this.page);
  }

  preview(): void {
    window.open(`http://localhost:51494${this.page.path}`)
  }

  saveDraft(): void {
    this.page.content = this.htmlEditor.first.content;

    if (!this.page.id) {
      this._pageService.add(this.page).subscribe(result => {
        this.toast.showSuccess('Page added successfully.', {});
      });
    } else {
      this._pageService.update(this.page).subscribe(result => {
        this.toast.showSuccess('Page updated successfully.', {});
      });
    }
  }

  publish(): void {
    this.page.content = this.htmlEditor.first.content;

    this._pageService.publish(this.page).subscribe(result => {
      this.toast.showSuccess('Page published successfully. You can now view you changes live.', {});
    });
  }

  applyExternalResource(externalResource: ExternalResource) {
    let pageScript: PageScript = new PageScript();

    pageScript.pageId = this.pageId;
    pageScript.externalResourceId = externalResource.id;
    pageScript.loadOrder = -1;

    this._pageScriptService.add(pageScript).subscribe((rtn) => {
      this.toast.showSuccess('External resource apply successfully.');
    })
  }
}
