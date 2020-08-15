import { Component, Input, OnInit } from '@angular/core';
import MenuItem from '../../../models/MenuItem';
import { MenuItemService } from '../../services/menu-item.service';

@Component({
  selector: 'menu-item-list',
  templateUrl: './menu-item-list.component.html',
  providers: [MenuItemService]
})
export class MenuItemListComponent implements OnInit {
  @Input() menuId: string;

  menuItems: MenuItem[] = [];

  constructor(private _menuItemService: MenuItemService) {
    
  }

  ngOnInit() {
    console.log(this.menuId);
    this._menuItemService.list(this.menuId).subscribe((menuItems) => this.menuItems = menuItems);
  }

  incrementOrder(menuItem: MenuItem, index: number) {
    this._menuItemService.incrementOrder(this.menuId, menuItem.id).subscribe(() => {
      let item: MenuItem = this.menuItems.splice(index, 1)[0];
      this.menuItems.splice(index++, 0, item);
    });
  }

  decrementOrder(menuItem: MenuItem, index: number) {
    this._menuItemService.incrementOrder(this.menuId, menuItem.id).subscribe(() => {
      let item: MenuItem = this.menuItems.splice(index, 1)[0];
      this.menuItems.splice(index--, 0, item);
    });
  }
}
