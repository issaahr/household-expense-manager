import { Stack, Typography } from '@mui/material';
import { useNavigate } from 'react-router-dom';

import { PersonForm } from '../components/person/PersonForm';
import { PersonTable } from '../components/person/PersonTable';
import { usePerson } from '../hooks/usePerson';
import { useTransaction } from '../hooks/useTransaction';

/**
 * Página responsável pelo gerenciamento de pessoas.
 */
export function PersonPage() {
  const navigate = useNavigate();

  const { person, createPerson, deletePerson } = usePerson();

  const { createTransaction } = useTransaction();

  return (
    <Stack spacing={4}>
      <Typography variant="h4">Pessoas</Typography>

      <PersonForm onSubmit={createPerson} />

      <PersonTable
        person={person}
        onDelete={deletePerson}
        onCreateTransaction={createTransaction}
        onViewTransactions={(id) => navigate(`/transactions/${id}`)}
      />
    </Stack>
  );
}
