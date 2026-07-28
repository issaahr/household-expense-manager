/**
 * Tipos de transação.
 */
export enum TransactionType {
  Expense = 0,
  Income = 1,
}

/**
 * Rótulos de transação.
 */
export const transactionTypeLabels = {
  [TransactionType.Expense]: 'Despesa',
  [TransactionType.Income]: 'Receita',
} as const;

/**
 * Payload para criação de transação.
 */
export interface CreateTransactionRequest {
  description: string;
  amount: number;
  type: TransactionType;
  personId: number;
}
