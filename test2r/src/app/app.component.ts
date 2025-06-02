import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { EventListComponent } from "./event-list/event-list.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, EventListComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'test2r';
}
