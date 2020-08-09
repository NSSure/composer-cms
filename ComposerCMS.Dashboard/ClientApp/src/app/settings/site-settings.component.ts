import { Component, OnInit } from '@angular/core';
import { SettingsService } from '../services/settings.service';
import { RouteService } from '../services/route.service';
import { Page } from '../../models/Page';
import Route from '../../models/Route';
import Settings from '../../models/Settings';

@Component({
  selector: 'app-site-settings',
  templateUrl: './site-settings.component.html',
  providers: [SettingsService, RouteService]
})
export class SiteSettingsComponent implements OnInit {
  settings: Settings = null;
  routes: Route[] = [];

  constructor(private _settingsService: SettingsService, private _routeService: RouteService) {

  }

  ngOnInit() {
    this._settingsService.get().subscribe((settings) => this.settings = settings);
    this._routeService.list().subscribe((routes) => this.routes = routes);
  }

  save() {
    this._settingsService.update(this.settings).subscribe(() => {
      alert('Site system settings updated successfully');
    })
  }

  purgeSystem() {
    this._settingsService.purgeSystem().subscribe((rtn) => {
      if (rtn) {
        window.location.reload();
      }
      else {
        alert('Failed to purge and reinitialize system. Please try again.');
      }
    })
  }
}
