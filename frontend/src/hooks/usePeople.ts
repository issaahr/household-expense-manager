import { useEffect, useState } from 'react';

import { personService } from '../services/personService';

import type { PersonResponse } from '../types/person';

/**
 * Hook responsável pelo estado e operações de pessoas.
 */
export function usePerson() {
  const [person, setPerson] = useState<PersonResponse[]>([]);

  async function loadPerson() {
    const data = await personService.getAll();

    setPerson(data);
  }

  async function createPerson(request: Parameters<typeof personService.create>[0]) {
    await personService.create(request);

    await loadPerson();
  }

  async function deletePerson(id: number) {
    await personService.remove(id);

    await loadPerson();
  }

  useEffect(() => {
    loadPerson();
  }, []);

  return {
    person,
    createPerson,
    deletePerson,
  };
}
