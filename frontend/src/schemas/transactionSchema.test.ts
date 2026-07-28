import { describe, expect, it } from 'vitest';

import { transactionSchema, TransactionType } from './transactionSchema';

describe('transactionSchema', () => {
  it('converte valor em formato brasileiro para número', () => {
    const result = transactionSchema.parse({
      description: 'Conta de luz',
      amount: '150,50',
      type: TransactionType.Expense,
      personId: 1,
    });

    expect(result.amount).toBe(150.5);
  });

  it('rejeita descrição contendo apenas espaços', () => {
    const result = transactionSchema.safeParse({
      description: '   ',
      amount: '10',
      type: TransactionType.Expense,
      personId: 1,
    });

    expect(result.success).toBe(false);
  });

  it('rejeita valor não numérico', () => {
    const result = transactionSchema.safeParse({
      description: 'Teste',
      amount: 'abc',
      type: TransactionType.Expense,
      personId: 1,
    });

    expect(result.success).toBe(false);
  });

  it('aceita valor com ponto decimal', () => {
    const result = transactionSchema.parse({
      description: 'Mercado',
      amount: '99.90',
      type: TransactionType.Expense,
      personId: 1,
    });

    expect(result.amount).toBe(99.9);
  });

  it('rejeita valor zero', () => {
    const result = transactionSchema.safeParse({
      description: 'Teste',
      amount: '0',
      type: TransactionType.Expense,
      personId: 1,
    });

    expect(result.success).toBe(false);
  });

  it('rejeita valor negativo', () => {
    const result = transactionSchema.safeParse({
      description: 'Teste',
      amount: '-10',
      type: TransactionType.Expense,
      personId: 1,
    });

    expect(result.success).toBe(false);
  });

  it('rejeita descrição vazia', () => {
    const result = transactionSchema.safeParse({
      description: '',
      amount: '10',
      type: TransactionType.Expense,
      personId: 1,
    });

    expect(result.success).toBe(false);
  });
});
