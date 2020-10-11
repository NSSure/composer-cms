import { Component, ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.css']
})
export class SideMenuComponent {
  @ViewChild('sideMenu', { static: true }) sideMenu: ElementRef;
  @ViewChild('menuTitle', { static: true }) menuTitle: ElementRef;

  isNarrowMenu: boolean = false;

  ngAfterViewInit(): void {
    var menuItems = this.sideMenu.nativeElement.querySelectorAll(".menu-item");
    var subMenus = this.sideMenu.nativeElement.querySelectorAll(".sub-menu");

    for (var i = 0; i < menuItems.length; i++) {
      menuItems[i].addEventListener('click', function () {
        var subMenu = this.getElementsByClassName("sub-menu")[0];
        if (subMenu) {
          if (subMenu.classList.contains("hidden")) {
            subMenu.classList.remove("hidden");
          }
          else {
            subMenu.classList.add("hidden");
          }
        }

        event.stopPropagation();
      }, false);
    }

    for (var i = 0; i < subMenus.length; i++) {
      subMenus[i].classList.add('hidden');
    }
  }

  toggleNarrowMenu() {
    if (this.sideMenu.nativeElement.classList.contains('side-menu-narrow')) {
      this.sideMenu.nativeElement.classList.remove('side-menu-narrow');
      this.menuTitle.nativeElement.classList.remove('hidden');
      this.isNarrowMenu = false;
    }
    else {
      this.sideMenu.nativeElement.classList.add('side-menu-narrow');
      this.menuTitle.nativeElement.classList.add('hidden');
      this.isNarrowMenu = true;
    }
  }
}
