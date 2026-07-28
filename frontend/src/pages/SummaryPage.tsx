import { Alert, CircularProgress, Stack, Typography } from '@mui/material';

import { SummaryTable } from '../components/summary/SummaryTable';
import { useSummary } from '../hooks/useSummary';

/**
 * Página responsável pela consulta
 * do resumo financeiro.s
 */
export function SummaryPage() {
  const { summary, loading, error } = useSummary();

  if (loading) {
    return <CircularProgress />;
  }

  return (
    <Stack spacing={4}>
      <Typography variant="h4">Resumo Financeiro</Typography>

      {error && <Alert severity="error">Não foi possível carregar o resumo financeiro.</Alert>}

      <SummaryTable summary={summary} />
    </Stack>
  );
}
