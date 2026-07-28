import { transactionService } from '../services/transactionService';
import type { CreateTransactionRequest } from '../types/transaction';

/**
 * Hook responsável pelas operações relacionadas
 * às transações.
 */
export function useTransactions() {
  /**
   * Cria uma nova transação.
   */
  async function createTransaction(request: CreateTransactionRequest) {
    await transactionService.create(request);
  }

  return {
    createTransaction,
  };
}
