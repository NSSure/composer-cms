import { Component, Input } from '@angular/core';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { AccountService } from '../../services/account.service';
import { filter, map, mergeMap } from 'rxjs/operators';

@Component({
  selector: 'app-page',
  templateUrl: './app-page.component.html'
})
export class AppPageComponent {
  @Input() hideBanner: boolean = false;

  pageTitle: string;

  constructor(private _router: Router, private _activatedRoute: ActivatedRoute, private _title: Title, private _accountService: AccountService) {
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
}
