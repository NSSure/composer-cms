import { Component, Input, OnInit } from '@angular/core';
import MenuItem from '../../../models/MenuItem';
import { MenuItemService } from '../../services/menu-item.service';

@Component({
  selector: 'menu-item-list',
  templateUrl: './menu-item-list.component.html',
  providers: [MenuItemService]
})
export class MenuItemListComponent implements OnInit {
  @Input() menuId: number;

  menuItems: MenuItem[] = [];

  constructor(private _menuItemService: MenuItemService) {
    
  }

  ngOnInit() {
    console.log(this.menuId);
    this._menuItemService.list(this.menuId).subscribe((menuItems) => this.menuItems = menuItems);
  }
}
