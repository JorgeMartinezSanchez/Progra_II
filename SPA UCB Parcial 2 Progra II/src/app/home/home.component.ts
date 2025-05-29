import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  public static list: any[][] = [];
  public static options_amount: number = 3;

  // Agregar el getter para list
  get list(): any[][] {
    return HomeComponent.list;
  }

  constructor(protected readonly router: Router) {
    // Inicializar la lista estática si está vacía
    if (HomeComponent.list.length === 0) {
      HomeComponent.list = Array(HomeComponent.options_amount).fill(null).map(() =>
        Array(HomeComponent.options_amount).fill(null).map(() => ({
          OccupiedSlot: false,
          Title: '',
          Cat: '',
          Desc: ''
        }))
      );
    }
  }

  goToSummitMenu() {
    this.router.navigate(['/summit']);
  }
}
