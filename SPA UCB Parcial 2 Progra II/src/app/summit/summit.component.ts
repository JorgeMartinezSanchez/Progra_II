import { Component } from '@angular/core';
import { HomeComponent } from '../home/home.component';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-summit',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './summit.component.html',
  styleUrl: './summit.component.css'
})
export class SummitComponent extends HomeComponent {
  constructor(protected override readonly router: Router) {
    super(router);
  }

  addSuggestion(title: string, category: string, desc: string) {
    if (!title || !category || !desc) {
      return; // Validación básica
    }

    for(let i = 0; i < HomeComponent.options_amount; i++) {
      for(let j = 0; j < HomeComponent.options_amount; j++) {
        if(!HomeComponent.list[i][j].OccupiedSlot) {
          HomeComponent.list[i][j].OccupiedSlot = true;
          HomeComponent.list[i][j].Title = title;
          HomeComponent.list[i][j].Cat = category;
          HomeComponent.list[i][j].Desc = desc;
          this.router.navigate(['/home']);
          return;
        }
      }
    }
  }
}
