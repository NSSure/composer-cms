import { Component } from '@angular/core';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { Title } from '@angular/platform-browser';

import { filter, map, mergeMap } from 'rxjs/operators';

import { AccountService } from './services/account.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html'
})
export class MainComponent {
  pageTitle: string;

  constructor(private _router: Router, private _activatedRoute: ActivatedRoute, private _title: Title, private _accountService: AccountService) {
    //_router.events.subscribe((event) => { //fires on every URL change
    //  _title.setTitle(_router.url);
    //});

    this._router.events.pipe(
      filter(e => e instanceof NavigationEnd),
      map(() => this._activatedRoute),
      map(route => {
        while (route.firstChild) {
          route = route.firstChild;
        }
        return route;
      }),
      mergeMap(route => route.data),
      map(data => {
        this.pageTitle = data.pageTitle;
        _title.setTitle(`${this.pageTitle} - Composer CMS`);
      }),
    ).subscribe();
  }

  logout() {
    this._accountService.logout();
  }
}
