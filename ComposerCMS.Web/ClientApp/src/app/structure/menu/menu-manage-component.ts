import { Component, OnInit, ViewChild } from '@angular/core';
import { Menu } from '../../../models/Menu';
import MenuItem from '../../../models/MenuItem';
import { ActivatedRoute } from '@angular/router';
import { MenuService } from '../../services/menu.service';
import { MenuItemService } from '../../services/menu-item.service';
import { MenuItemListComponent } from './menu-item-list.component';
import { RouteService } from '../../services/route.service';
import Route from '../../../models/Route';

@Component({
  selector: 'app-menu-manage',
  templateUrl: './menu-manage.component.html',
  providers: [MenuService, MenuItemService, RouteService]
})
export class MenuManageComponent implements OnInit {
  menuId?: string;
  menu: Menu = new Menu();
  menuItem: MenuItem = new MenuItem();
  routes: Route[] = [];
  isMenuItem: boolean = false;

  @ViewChild(MenuItemListComponent, { static: false }) menuItemListComponent: MenuItemListComponent;

  constructor(
    private _activatedRoute: ActivatedRoute,
    private _menuService: MenuService,
    private _menuItemService: MenuItemService,
    private _routeService: RouteService) {
    this._activatedRoute.params.subscribe((params) => {
      this.menuId = params.menuId;
    });
  }

  ngOnInit() {
    if (this.menuId) {
      this._menuService.get(this.menuId).subscribe((menu) => this.menu = menu);
    }

    this._routeService.list().subscribe((routes) => this.routes = routes);
  }

  save() {
    if (this.menu.id) {
      this._menuService.update(this.menu).subscribe(() => {
        alert('Menu updated successfully');
      })
    }
    else {
      this._menuService.add(this.menu).subscribe(() => {
        alert('Menu added successfully');
      })
    }
  }

  saveMenuItem() {
    if (this.menuItem.id) {
      this._menuItemService.update(this.menuItem).subscribe(() => {
        let menuItem = this.menuItemListComponent.menuItems.find((menuItem) => menuItem.id === this.menuItem.id);
        menuItem.displayText = this.menuItem.displayText;

        alert('Menu item updated successfully');

        this.isMenuItem = false;
      });
    }
    else {
      this.menuItem.menuID = this.menuId;

      this._menuItemService.add(this.menuItem).subscribe(() => {
        this.menuItemListComponent.menuItems.push(this.menuItem);
        this.menuItem = new MenuItem();

        alert('Menu item add successfully');

        this.isMenuItem = false;
      });
    }
  }
}
