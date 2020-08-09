import { Component, Input, OnInit } from '@angular/core';
import MenuItem from '../../../models/MenuItem';
import { MenuItemService } from '../../services/menu-item.service';

@Component({
  selector: 'menu-item-list',
  templateUrl: './menu-item-list.component.html',
  providers: [MenuItemService]
})
export class MenuItemListComponent implements OnInit {
  @Input() menuItemId: number;

  menuItems: MenuItem[] = [];

  constructor(private _menuItemService: MenuItemService) {
    
  }

  ngOnInit() {
    this._menuItemService.list(this.menuItemId).subscribe((menuItems) => this.menuItems = menuItems);
  }
}
