import { Component, ViewChild, ElementRef, OnInit } from '@angular/core';
import { SettingsService } from '../../services/settings.service';
import DashboardStats from '../../models/DashboardStats';

@Component({
  selector: 'dashboard',
  templateUrl: './dashboard.component.html',
  providers: [SettingsService]
})
export class DashboardComponent implements OnInit {
  dashboardStats: DashboardStats;

  constructor(private _settingsService: SettingsService) {

  }

  ngOnInit() {
    this._settingsService.dashboardStats().subscribe((dashboardStats) => this.dashboardStats = dashboardStats);
  }
}
