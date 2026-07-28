import { act, renderHook, waitFor } from '@testing-library/react';
import { beforeEach, describe, expect, it, vi } from 'vitest';

import { personService } from '../services/personService';
import { transactionService } from '../services/transactionService';
import { usePerson } from './usePerson';

vi.mock('../services/personService', () => ({
  personService: {
    getAll: vi.fn(),
    create: vi.fn(),
    remove: vi.fn(),
  },
}));

vi.mock('../services/transactionService', () => ({
  transactionService: {
    create: vi.fn(),
  },
}));

describe('usePerson', () => {
  beforeEach(() => {
    vi.clearAllMocks();

    vi.mocked(personService.getAll).mockResolvedValue([
      {
        id: 1,
        name: 'Maria',
        age: 20,
        createdAt: '2026-01-01',
      },
    ]);
  });

  it('carrega as pessoas ao iniciar', async () => {
    const { result } = renderHook(() => usePerson());

    await waitFor(() => {
      expect(result.current.person).toHaveLength(1);
    });

    expect(personService.getAll).toHaveBeenCalledTimes(1);
  });

  it('cria uma pessoa e recarrega a lista', async () => {
    vi.mocked(personService.create).mockResolvedValue({
      id: 2,
      name: 'João',
      age: 18,
      createdAt: '2026-01-01',
    });

    const { result } = renderHook(() => usePerson());

    await waitFor(() => expect(result.current.person).toHaveLength(1));

    await act(async () => {
      await result.current.createPerson({
        name: 'João',
        birthDate: '2008-01-01',
      });
    });

    expect(personService.create).toHaveBeenCalled();

    expect(personService.getAll).toHaveBeenCalledTimes(2);
  });

  it('remove uma pessoa e recarrega a lista', async () => {
    vi.mocked(personService.remove).mockResolvedValue();

    const { result } = renderHook(() => usePerson());

    await waitFor(() => expect(result.current.person).toHaveLength(1));

    await act(async () => {
      await result.current.deletePerson(1);
    });

    expect(personService.remove).toHaveBeenCalledWith(1);

    expect(personService.getAll).toHaveBeenCalledTimes(2);
  });

  it('cria uma transação e recarrega a lista', async () => {
    vi.mocked(transactionService.create).mockResolvedValue();

    const { result } = renderHook(() => usePerson());

    await waitFor(() => expect(result.current.person).toHaveLength(1));

    await act(async () => {
      await result.current.createTransaction({
        personId: 1,
        description: 'Conta',
        amount: 100,
        type: 0,
      });
    });

    expect(transactionService.create).toHaveBeenCalled();

    expect(personService.getAll).toHaveBeenCalledTimes(2);
  });

  it('permite recarregar manualmente', async () => {
    const { result } = renderHook(() => usePerson());

    await waitFor(() => expect(result.current.person).toHaveLength(1));

    await act(async () => {
      await result.current.reload();
    });

    expect(personService.getAll).toHaveBeenCalledTimes(2);
  });
});
