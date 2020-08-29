import { Component } from '@angular/core';
import { Menu } from '../../../models/Menu';
import { MenuService } from '../../../services/menu.service';

@Component({
  selector: 'app-menu-list',
  templateUrl: './menu-list.component.html',
  providers: [MenuService]
})
export class MenuListComponent {
  menus: Menu[] = [];

  constructor(private _menuService: MenuService) {
    this._menuService.list().subscribe((menus) => this.menus = menus);
  }
}
