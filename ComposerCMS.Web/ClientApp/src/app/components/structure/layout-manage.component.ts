import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http'
import { LayoutService } from '../../services/layout.service';

@Component({
  selector: 'app-layout-manage',
  templateUrl: './layout-manage.component.html',
  providers: [LayoutService]
})
export class LayoutManageComponent implements OnInit {
  constructor(private _layoutService: LayoutService) {

  }

  ngOnInit(): void {

  }

  saveDraft(): void {

  }

  publish(): void {

  }
}
