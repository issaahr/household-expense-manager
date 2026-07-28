import { act, renderHook, waitFor } from '@testing-library/react';
import { beforeEach, describe, expect, it, vi } from 'vitest';

import { transactionService } from '../services/transactionService';
import { useTransaction } from './useTransaction';

vi.mock('../services/transactionService', () => ({
  transactionService: {
    getAll: vi.fn(),
    getByPerson: vi.fn(),
    create: vi.fn(),
  },
}));

describe('useTransaction', () => {
  beforeEach(() => {
    vi.clearAllMocks();

    vi.mocked(transactionService.getAll).mockResolvedValue([]);

    vi.mocked(transactionService.getByPerson).mockResolvedValue([]);

    vi.mocked(transactionService.create).mockResolvedValue({
      id: 1,
      description: 'Conta',
      amount: 100,
      type: 0,
      personId: 1,
      personName: 'Maria',
    });
  });

  it('carrega todas as transações quando não recebe pessoa', async () => {
    vi.mocked(transactionService.getAll).mockResolvedValue([
      {
        id: 1,
        description: 'Conta',
        amount: 100,
        type: 1,
        personId: 1,
        personName: 'Maria',
      },
    ]);

    const { result } = renderHook(() => useTransaction());

    await waitFor(() => {
      expect(result.current.transactions).toHaveLength(1);
    });

    expect(transactionService.getAll).toHaveBeenCalled();
  });

  it('carrega apenas as transações da pessoa informada', async () => {
    const { result } = renderHook(() => useTransaction(1));

    await waitFor(() => {
      expect(transactionService.getByPerson).toHaveBeenCalledWith(1);
    });

    expect(result.current.transactions).toEqual([]);
  });

  it('cria uma transação e recarrega os dados', async () => {
    const { result } = renderHook(() => useTransaction());

    const request = {
      personId: 1,
      description: 'Conta',
      amount: 100,
      type: 0,
    };

    await act(async () => {
      await result.current.createTransaction(request);
    });

    expect(transactionService.create).toHaveBeenCalledWith(request);

    expect(transactionService.getAll).toHaveBeenCalled();
  });
});
