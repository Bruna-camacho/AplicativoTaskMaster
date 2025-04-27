import { Component } from '@angular/core';
import { TarefaComponent } from './paginas/tarefa/tarefa.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [TarefaComponent],
  template: `<app-tarefa></app-tarefa>`,
})
export class AppComponent { }