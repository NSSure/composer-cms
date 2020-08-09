import { Component, OnInit } from '@angular/core';
import { Theme } from 'src/models/Theme';
import { SettingsService } from 'src/app/services/settings.service';

@Component({
  selector: 'app-theme-list',
  templateUrl: './theme-list.component.html',
  providers: [SettingsService]
})
export class ThemeListComponent implements OnInit {
  themes: Theme[] = [];
  currentThemeKey: string;

  constructor(private _settingsService: SettingsService) {

  }

  ngOnInit(): void {
    this._settingsService.getTheme().subscribe((themeKey) => this.currentThemeKey = themeKey);
    this._settingsService.listThemes().subscribe((themes) => this.themes = themes);
  }

  isThemeApplied(themeKey): boolean {
    return this.currentThemeKey == themeKey;
  }

  apply(themeKey: string) {
    this._settingsService.setTheme(themeKey).subscribe(() => {
      this.currentThemeKey = themeKey;
    });
  }
}
