/**
 * Interface para representar o saldo de um usuário.
 */
export interface PersonBalanceResponse {
  personId: number;
  name: string;
  totalIncome: number;
  totalExpense: number;
  balance: number;
}

/**
 * Interface para representar o resumo financeiro de todos os usuários.
 */
export interface PersonFinancialSummaryResponse {
  people: PersonBalanceResponse[];
  totalIncome: number;
  totalExpense: number;
  balance: number;
}
