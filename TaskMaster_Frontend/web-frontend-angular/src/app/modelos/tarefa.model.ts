export interface Tarefa {
    id: number;
    descricao: string;
    dataHora: string;
    prioridade: 'baixa' | 'média' | 'alta';
    concluida: boolean;
  }
  