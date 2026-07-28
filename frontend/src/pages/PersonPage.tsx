import { Stack, Typography } from '@mui/material';

import { PersonForm } from '../components/person/PersonForm';
import { PersonTable } from '../components/person/PersonTable';
import { usePerson } from '../hooks/usePerson';
import { useTransactions } from '../hooks/useTransaction';

/**
 * Página responsável pelo gerenciamento de pessoas.
 */
export function PersonPage() {
  const { person, createPerson, deletePerson } = usePerson();

  const { createTransaction } = useTransactions();

  return (
    <Stack spacing={4}>
      <Typography variant="h4">Pessoas</Typography>

      <PersonForm onSubmit={createPerson} />

      <PersonTable person={person} onDelete={deletePerson} onCreateTransaction={createTransaction} />
    </Stack>
  );
}
