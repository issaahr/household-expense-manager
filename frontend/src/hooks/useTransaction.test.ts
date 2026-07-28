import { act, renderHook } from '@testing-library/react';
import { beforeEach, describe, expect, it, vi } from 'vitest';

import { transactionService } from '../services/transactionService';
import { useTransactions } from './useTransaction';

vi.mock('../services/transactionService', () => ({
  transactionService: {
    create: vi.fn(),
  },
}));

describe('useTransactions', () => {
  beforeEach(() => {
    vi.clearAllMocks();

    vi.mocked(transactionService.create).mockResolvedValue();
  });

  it('cria uma transação', async () => {
    const { result } = renderHook(() => useTransactions());

    await act(async () => {
      await result.current.createTransaction({
        personId: 1,
        description: 'Conta',
        amount: 100,
        type: 0,
      });
    });

    expect(transactionService.create).toHaveBeenCalledWith({
      personId: 1,
      description: 'Conta',
      amount: 100,
      type: 0,
    });
  });
});
