import { Component, Output } from '@angular/core';
import { Evento } from '../evento';
import { OnInit } from '@angular/core';

@Component({
  selector: 'app-event-list',
  imports: [],
  templateUrl: './event-list.component.html',
  styleUrl: './event-list.component.css'
})
export class EventListComponent implements OnInit {
  public static list: Evento[][] = [];

  ngOnInit(): void {
    for(let i=0; i<201; i++){
      EventListComponent.list[i] = [];
      for(let j=0; j<201; j++){
        EventListComponent.list[i][j];
      }
    }
  }

  create(name: string, desc: string, date: string, type: string, ste: string){
    for(let i=0; i<201; i++){
      for(let j=0; j<201; j++){
        if(EventListComponent.list[i][j] == null){
          EventListComponent.list[i][j] = new Evento(name, desc, date, type, ste);
        }
      }
    }
  }

  edit(index_X: number, index_Y: number, name: string, desc: string, date: string, type: string, ste: string){
    EventListComponent.list[index_X][index_Y].editTitle(name);
    EventListComponent.list[index_X][index_Y].editDescription(desc);
    EventListComponent.list[index_X][index_Y].editDate(date);
    EventListComponent.list[index_X][index_Y].editType(type);
    EventListComponent.list[index_X][index_Y].editEstado(ste);
  }

  delete(index_X: number, index_Y: number){
    EventListComponent.list[index_X][index_Y] == null;
  }
}
