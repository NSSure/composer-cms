import { Component, Input } from '@angular/core';

@Component({
  selector: 'active-state-indicator',
  templateUrl: './active-state-indicator.component.html'
})
export class ActiveStateIndicatorComponent {
  @Input() flag: boolean;
}
