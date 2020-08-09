import { Component, OnInit } from '@angular/core';
import { ActivityHistoryService } from '../services/activity-history.service';
import { ActivityHistory } from '../../models/ActivityHistory';

@Component({
  selector: 'app-activity-history',
  templateUrl: './activity-history.component.html',
  providers: [ActivityHistoryService]
})
export class ActivityHistoryComponent implements OnInit {
  activityHistory: ActivityHistory[] = [];

  constructor(private _activityHistoryService: ActivityHistoryService) {

  }

  ngOnInit() {
    this._activityHistoryService.list().subscribe((activityHistory) => this.activityHistory = activityHistory);
  }
}
