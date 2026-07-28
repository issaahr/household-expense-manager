import type { CreateTransactionRequest, TransactionResponse } from '../types/transaction';
import { api } from './api';

/**
 * Serviço responsável pelas operações relacionadas às transações.
 */
export const transactionService = {
  /**
   * Lista todas as transações cadastradas.
   */
  async getAll(): Promise<TransactionResponse[]> {
    const response = await api.get<TransactionResponse[]>('/api/Transaction');

    return response.data;
  },

  /**
   * Lista as transações vinculadas a uma pessoa.
   */
  async getByPerson(personId: number): Promise<TransactionResponse[]> {
    const response = await api.get<TransactionResponse[]>(`/api/Transaction/person/${personId}`);

    return response.data;
  },

  /**
   * Cria uma nova transação.
   */
  async create(request: CreateTransactionRequest): Promise<TransactionResponse> {
    const response = await api.post<TransactionResponse>('/api/Transaction', request);

    return response.data;
  },
};
