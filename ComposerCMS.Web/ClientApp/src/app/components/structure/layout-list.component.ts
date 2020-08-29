import { Component, OnInit } from '@angular/core';
import { LayoutService } from '../../services/layout.service';

@Component({
  selector: 'app-layout-list',
  templateUrl: './layout-list.component.html',
  providers: [LayoutService]
})
export class LayoutListComponent implements OnInit {
  layouts: Array<any> = new Array();

  constructor(private _layoutService: LayoutService) {

  }

  ngOnInit(): void {

  }
}
