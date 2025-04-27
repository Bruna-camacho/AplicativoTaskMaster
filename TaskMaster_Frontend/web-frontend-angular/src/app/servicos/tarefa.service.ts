import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Tarefa } from '../modelos/tarefa.model';

@Injectable({
  providedIn: 'root'
})
export class TarefaService {
  private apiUrl = 'http://localhost:44339/api/tarefas';

  constructor(private http: HttpClient) { }

  listarTarefas(): Observable<Tarefa[]> {
    return this.http.get<Tarefa[]>(this.apiUrl);
  }

  buscarTarefaPorId(id: number): Observable<Tarefa> {
    return this.http.get<Tarefa>(`${this.apiUrl}/${id}`);
  }

  buscarPorDescricao(descricao: string): Observable<Tarefa[]> {
    return this.http.get<Tarefa[]>(`${this.apiUrl}/descricao?descricao=${descricao}`);
  }

  buscarPorData(data: string): Observable<Tarefa[]> {
    return this.http.get<Tarefa[]>(`${this.apiUrl}/data?data=${data}`);
  }

  buscarPorPrioridade(prioridade: string): Observable<Tarefa[]> {
    return this.http.get<Tarefa[]>(`${this.apiUrl}/prioridade?prioridade=${prioridade}`);
  }

  criarTarefa(tarefa: Tarefa): Observable<any> {
    return this.http.post(this.apiUrl, tarefa);
  }

  atualizarTarefa(id: number, tarefa: Tarefa): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, tarefa);
  }

  deletarTarefa(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  concluirTarefa(id: number): Observable<any> {
    return this.http.patch(`${this.apiUrl}/${id}/concluir`, {});
  }
}