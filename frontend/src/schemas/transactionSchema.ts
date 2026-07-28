import { z } from 'zod';

export enum TransactionType {
  Expense = 0,
  Income = 1,
}

/**
 * Converte valores digitados como moeda brasileira
 * para número antes da validação.
 */
const currencyInput = z
  .string()
  .transform((value) => Number(value.replace(',', '.')))
  .refine((value) => Number.isFinite(value), {
    message: 'O valor da transação deve ser um número válido',
  })
  .refine((value) => value > 0, {
    message: 'O valor da transação deve ser positivo',
  });

/**
 * Validação do formulário de transação, espelhando as regras aplicadas
 * pelo CreateTransactionRequestValidator no backend (FluentValidation):
 * - Descrição: obrigatória, 2 a 500 caracteres.
 * - Valor: deve ser positivo.
 * - Tipo: deve ser um valor válido do enum de transação.
 * - Pessoa: deve possuir um identificador válido.
 */
export const transactionSchema = z.object({
  description: z
    .string()
    .trim()
    .min(2, 'Descrição deve possuir pelo menos 2 caracteres')
    .max(500, 'Descrição deve possuir no máximo 500 caracteres'),

  amount: currencyInput,

  type: z.union([z.literal(TransactionType.Expense), z.literal(TransactionType.Income)], {
    error: 'Tipo de transação inválido',
  }),

  personId: z.number().positive('A pessoa deve possuir um ID válido'),
});

/**
 * Dados antes da transformação do Zod.
 * Representa o que vem dos inputs.
 */
export type TransactionFormInput = z.input<typeof transactionSchema>;

/**
 * Dados após validação e transformação.
 * Representa o que será enviado para API.
 */
export type TransactionFormValues = z.output<typeof transactionSchema>;
