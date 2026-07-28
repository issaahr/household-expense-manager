import { useCallback, useEffect, useState } from 'react';

import { transactionService } from '../services/transactionService';
import type { CreateTransactionRequest, TransactionResponse } from '../types/transaction';

/**
 * Centraliza o gerenciamento de transações.
 *
 * Responsabilidades:
 * - carregar transações;
 * - criar transações;
 * - filtrar carregamento por pessoa quando necessário.
 */
export function useTransaction(personId?: number) {
  const [transactions, setTransactions] = useState<TransactionResponse[]>([]);

  const [loading, setLoading] = useState(false);

  const [error, setError] = useState(false);

  /**
   * Carrega as transações disponíveis.
   *
   * Quando informado um identificador de pessoa,
   * retorna apenas as transações vinculadas a ela.
   */
  const loadTransactions = useCallback(async () => {
    setLoading(true);

    try {
      setError(false);

      const data =
        personId !== undefined ? await transactionService.getByPerson(personId) : await transactionService.getAll();

      setTransactions(data);
    } catch (error) {
      if (import.meta.env.DEV) {
        console.error(error);
      }

      setError(true);
    } finally {
      setLoading(false);
    }
  }, [personId]);

  /**
   * Cria uma nova transação.
   */
  async function createTransaction(request: CreateTransactionRequest) {
    await transactionService.create(request);

    await loadTransactions();
  }

  useEffect(() => {
    void loadTransactions();
  }, [loadTransactions]);

  return {
    transactions,
    loading,
    error,
    createTransaction,
    reload: loadTransactions,
  };
}
