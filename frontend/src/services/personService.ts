import type { CreatePersonRequest, PersonResponse } from '../types/person';
import type { PersonFinancialSummaryResponse } from '../types/summary';
import { api } from './api';

/**
 * Serviço responsável pelas operações relacionadas às pessoas.
 */
export const personService = {
  /**
   * Lista todas as pessoas cadastradas.
   */
  async getAll(): Promise<PersonResponse[]> {
    const response = await api.get<PersonResponse[]>('/api/Person');

    return response.data;
  },

  /**
   * Cria uma nova pessoa.
   */
  async create(request: CreatePersonRequest): Promise<PersonResponse> {
    const response = await api.post<PersonResponse>('/api/Person', request);

    return response.data;
  },

  /**
   * Remove uma pessoa pelo identificador.
   */
  async remove(id: number): Promise<void> {
    await api.delete(`/api/Person/${id}`);
  },

  /**
   * Obtém o resumo financeiro das pessoas cadastradas.
   */
  async getFinancialSummary(): Promise<PersonFinancialSummaryResponse> {
    const response = await api.get<PersonFinancialSummaryResponse>('/api/Person/financial-summary');

    return response.data;
  },
};
