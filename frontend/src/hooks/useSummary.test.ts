import { act, renderHook, waitFor } from '@testing-library/react';
import { beforeEach, describe, expect, it, vi } from 'vitest';

import { personService } from '../services/personService';
import { useSummary } from './useSummary';

vi.mock('../services/personService', () => ({
  personService: {
    getFinancialSummary: vi.fn(),
  },
}));

describe('useSummary', () => {
  beforeEach(() => {
    vi.clearAllMocks();
  });

  it('carrega o resumo financeiro', async () => {
    vi.mocked(personService.getFinancialSummary).mockResolvedValue({
      people: [],
      totalIncome: 100,
      totalExpense: 50,
      balance: 50,
    });

    const { result } = renderHook(() => useSummary());

    await waitFor(() => {
      expect(result.current.loading).toBe(false);
    });

    expect(result.current.error).toBe(false);

    expect(result.current.summary).toEqual({
      people: [],
      totalIncome: 100,
      totalExpense: 50,
      balance: 50,
    });
  });

  it('define erro quando a requisição falha', async () => {
    vi.mocked(personService.getFinancialSummary).mockRejectedValue(new Error('erro'));

    const { result } = renderHook(() => useSummary());

    await waitFor(() => {
      expect(result.current.loading).toBe(false);
    });

    expect(result.current.error).toBe(true);
    expect(result.current.summary).toBeNull();
  });

  it('recarrega o resumo financeiro', async () => {
    vi.mocked(personService.getFinancialSummary).mockResolvedValue({
      people: [],
      totalIncome: 0,
      totalExpense: 0,
      balance: 0,
    });

    const { result } = renderHook(() => useSummary());

    await waitFor(() => {
      expect(result.current.loading).toBe(false);
    });

    await act(async () => {
      await result.current.reload();
    });

    expect(personService.getFinancialSummary).toHaveBeenCalledTimes(2);
  });
});
