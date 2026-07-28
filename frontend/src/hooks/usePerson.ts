import { useEffect, useState } from 'react';

import { personService } from '../services/personService';
import { transactionService } from '../services/transactionService';
import type { CreatePersonRequest, PersonResponse } from '../types/person';
import type { CreateTransactionRequest } from '../types/transaction';

/**
 * Centraliza o gerenciamento de pessoas e transações.
 *
 * Responsabilidades:
 * - carregar pessoas;
 * - criar pessoas;
 * - excluir pessoas;
 * - criar transações;
 * - manter os dados sincronizados com a API.
 */
export function usePerson() {
  const [person, setPerson] = useState<PersonResponse[]>([]);

  async function loadPerson() {
    const data = await personService.getAll();

    setPerson(data);
  }

  async function createPerson(request: CreatePersonRequest) {
    await personService.create(request);

    await loadPerson();
  }

  async function deletePerson(id: number) {
    await personService.remove(id);

    await loadPerson();
  }

  async function createTransaction(request: CreateTransactionRequest) {
    await transactionService.create(request);

    /**
     * A idade não muda, mas no futuro a tabela
     * poderá apresentar saldo/resumo.
     */
    await loadPerson();
  }

  useEffect(() => {
    void loadPerson();
  }, []);

  return {
    person,
    createPerson,
    deletePerson,
    createTransaction,
    reload: loadPerson,
  };
}
