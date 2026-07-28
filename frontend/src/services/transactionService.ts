import type { CreateTransactionRequest } from '../types/transaction';
import { api } from './api';

/**
 * Serviço responsável pela comunicação
 * com os endpoints de transações.
 */
export const transactionService = {
  /**
   * Cria uma nova transação.
   */
  async create(request: CreateTransactionRequest): Promise<void> {
    await api.post('/api/Transaction', request);
  },
};
