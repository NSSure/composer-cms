import { Component, OnInit } from '@angular/core';
import { SettingsService } from '../services/settings.service';
import { RouteService } from '../services/route.service';
import { Page } from '../../models/Page';
import Route from '../../models/Route';
import Settings from '../../models/Settings';
import { Theme } from '../../models/Theme';

@Component({
  selector: 'app-site-settings',
  templateUrl: './site-settings.component.html',
  providers: [SettingsService, RouteService]
})
export class SiteSettingsComponent implements OnInit {
  settings: Settings = null;
  routes: Route[] = [];
  themes: Theme[] = [];

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

  syncFileSystem() {
    this._settingsService.syncFileSystem().subscribe((rtn) => {
      if (rtn) {
        alert('Successfully synced the file system.');
      }
      else {
        alert('Failed to sync the file system. Please try again.');
      }
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
    });
  }
}
