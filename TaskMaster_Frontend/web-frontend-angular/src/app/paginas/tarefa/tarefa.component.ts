import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TarefaService } from '../../servicos/tarefa.service';
import { Tarefa } from '../../modelos/tarefa.model';

@Component({
  selector: 'app-tarefa',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './tarefa.component.html',
  styleUrls: ['./tarefa.component.css']
})
export class TarefaComponent implements OnInit {
  tarefas: Tarefa[] = [];
  tarefasFiltradas: Tarefa[] = [];

  novaTarefa = {
    descricao: '',
    dataHora: '',
    prioridade: 'baixa'
  };

  filtroPrioridades: string[] = [];
  filtroData: string = '';
  termoBusca: string = '';
  filtroAtivo: boolean = false;

  editandoTarefa: boolean = false;
  tarefaEmEdicaoId: number | null = null;

  constructor(private tarefaService: TarefaService) { }

  ngOnInit(): void {
    document.body.classList.add('light-theme');
    this.listarTarefas();
  }
  
  listarTarefas(): void {
    this.tarefaService.listarTarefas().subscribe({
      next: (dados) => {
        this.tarefas = dados.map((tarefa: any) => ({
          id: tarefa.Id,
          descricao: tarefa.Descricao,
          dataHora: tarefa.DataHora,
          prioridade: tarefa.Prioridade,
          concluida: tarefa.Concluida
        }));

        this.tarefas.sort((a: Tarefa, b: Tarefa) => {
          return new Date(b.dataHora).getTime() - new Date(a.dataHora).getTime();
        });

        console.log('Tarefas recebidas e ordenadas:', this.tarefas);
      },
      error: (erro) => {
        console.error('Erro ao buscar tarefas:', erro);
      }
    });
  }

  criarTarefa(): void {
    
    if (!this.novaTarefa.descricao.trim()) {
      alert('Por favor, preencha o campo Descrição.');
      return;
    }
    if (!this.novaTarefa.dataHora) {
      alert('Por favor, preencha o campo Data e Hora.');
      return;
    }
    if (!this.novaTarefa.prioridade) {
      alert('Por favor, selecione uma Prioridade.');
      return;
    }
  
    if (this.editandoTarefa && this.tarefaEmEdicaoId !== null) {
      const tarefaAtualizada: Tarefa = {
        id: this.tarefaEmEdicaoId,
        descricao: this.novaTarefa.descricao,
        dataHora: this.novaTarefa.dataHora,
        prioridade: this.novaTarefa.prioridade as 'baixa' | 'média' | 'alta',
        concluida: false
      };
  
      this.tarefaService.atualizarTarefa(this.tarefaEmEdicaoId, tarefaAtualizada).subscribe({
        next: () => {
          console.log('Tarefa atualizada com sucesso!');
          this.novaTarefa = { descricao: '', dataHora: '', prioridade: 'baixa' };
          this.editandoTarefa = false;
          this.tarefaEmEdicaoId = null;
          this.listarTarefas();
        },
        error: (erro) => {
          console.error('Erro ao atualizar tarefa:', erro);
          alert('Ocorreu um erro ao atualizar a tarefa.');
        }
      });
  
    } else {
      const tarefaParaEnviar: Tarefa = {
        id: 0,
        descricao: this.novaTarefa.descricao,
        dataHora: this.novaTarefa.dataHora,
        prioridade: this.novaTarefa.prioridade as 'baixa' | 'média' | 'alta',
        concluida: false
      };
  
      this.tarefaService.criarTarefa(tarefaParaEnviar).subscribe({
        next: () => {
          console.log('Tarefa criada com sucesso!');
          this.novaTarefa = { descricao: '', dataHora: '', prioridade: 'baixa' };
          this.listarTarefas();
        },
        error: (erro) => {
          console.error('Erro ao criar tarefa:', erro);
          alert('Ocorreu um erro ao criar a tarefa.');
        }
      });
    }
  }
  
  editarTarefa(tarefa: Tarefa): void {
    this.novaTarefa = {
      descricao: tarefa.descricao,
      dataHora: tarefa.dataHora,
      prioridade: tarefa.prioridade
    };
    this.editandoTarefa = true;
    this.tarefaEmEdicaoId = tarefa.id;
  }

  concluirTarefa(id: number): void {
    this.tarefaService.concluirTarefa(id).subscribe({
      next: () => {
        console.log('Tarefa concluída com sucesso!');
        this.listarTarefas();
      },
      error: (erro) => {
        console.error('Erro ao concluir tarefa:', erro);
      }
    });
  }

  excluirTarefa(id: number): void {
    if (confirm('Tem certeza que deseja excluir esta tarefa?')) {
      this.tarefaService.deletarTarefa(id).subscribe({
        next: () => {
          console.log('Tarefa excluída com sucesso!');
          this.listarTarefas();
        },
        error: (erro) => {
          console.error('Erro ao excluir tarefa:', erro);
        }
      });
    }
  }

  aplicarFiltros(): void {
    if (this.termoBusca.trim() !== '' && this.termoBusca.trim().length < 3) {
      alert('Digite no mínimo 3 caracteres para buscar pela descrição.');
      return;
    }
  
    this.tarefasFiltradas = this.tarefas.filter((tarefa: Tarefa) => {
      const prioridadeOk = this.filtroPrioridades.length === 0 || this.filtroPrioridades.includes(tarefa.prioridade.toLowerCase());
      const dataOk = !this.filtroData || tarefa.dataHora.startsWith(this.filtroData);
      const buscaOk = !this.termoBusca || tarefa.descricao.toLowerCase().includes(this.termoBusca.toLowerCase());
      return prioridadeOk && dataOk && buscaOk;
    });
  
    this.tarefasFiltradas.sort((a: Tarefa, b: Tarefa) => {
      return new Date(b.dataHora).getTime() - new Date(a.dataHora).getTime();
    });
  
    this.filtroAtivo = true;
  }
  
  limparFiltros(): void {
    this.filtroPrioridades = [];
    this.filtroData = '';
    this.termoBusca = '';
    this.tarefasFiltradas = [];
    this.filtroAtivo = false;
  }
  
alternarTema() {
  const body = document.body;
  body.classList.toggle('dark-theme');
  body.classList.toggle('light-theme');
}

}