import { Component, OnInit } from '@angular/core';
import { ActivityHistory } from '../../models/ActivityHistory';
import { ActivityHistoryService } from '../../services/activity-history.service';

@Component({
  selector: 'app-activity-history',
  templateUrl: './activity-history.component.html',
  providers: [ActivityHistoryService]
})
export class ActivityHistoryComponent implements OnInit {
  activityHistory: ActivityHistory[] = [];
  entity: string = '';

  constructor(private _activityHistoryService: ActivityHistoryService) {

  }

  ngOnInit() {
    this._activityHistoryService.list().subscribe((activityHistory) => this.activityHistory = activityHistory);
  }

  filterActivity = (activity: ActivityHistory) => {
    if (this.entity == '') {
      return true;
    }

    return activity.entity === this.entity;
  }
}
