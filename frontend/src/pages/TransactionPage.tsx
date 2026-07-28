import { Stack, Typography } from '@mui/material';
import { useParams } from 'react-router-dom';

import { TransactionTable } from '../components/transaction/TransactionTable';
import { useTransaction } from '../hooks/useTransaction';

/**
 * Página responsável pela consulta de transações.
 */
export function TransactionPage() {
  const { personId } = useParams();

  const id = personId ? Number(personId) : undefined;

  const { transactions } = useTransaction(id);

  return (
    <Stack spacing={4}>
      <Typography variant="h4">{id !== undefined ? 'Transações da pessoa' : 'Todas as transações'}</Typography>

      <TransactionTable transactions={transactions} />
    </Stack>
  );
}
