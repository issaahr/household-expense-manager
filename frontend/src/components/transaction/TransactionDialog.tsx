import { Alert, Dialog, DialogContent, DialogTitle } from '@mui/material';
import { useState } from 'react';

import type { CreateTransactionRequest } from '../../types/transaction';
import { TransactionForm } from './TransactionForm';

interface TransactionDialogProps {
  open: boolean;
  personId: number;
  onClose(): void;
  onSubmit(request: CreateTransactionRequest): Promise<void>;
}

/**
 * Modal responsável pelo cadastro de uma transação vinculada
 * a uma pessoa específica.
 *
 * Mantém o formulário aberto em caso de erro para permitir
 * correção dos dados ou nova tentativa.
 */
export function TransactionDialog({ open, personId, onClose, onSubmit }: TransactionDialogProps) {
  const [error, setError] = useState(false);

  async function handleSubmit(request: CreateTransactionRequest) {
    try {
      setError(false);

      await onSubmit({
        ...request,
        personId,
      });

      onClose();
    } catch (error) {
      if (import.meta.env.DEV) {
        console.error('Erro ao criar transação:', error);
      }

      setError(true);
    }
  }

  function handleClose() {
    setError(false);
    onClose();
  }

  return (
    <Dialog open={open} onClose={handleClose} fullWidth maxWidth="sm">
      <DialogTitle>Nova transação</DialogTitle>

      <DialogContent>
        {error && (
          <Alert severity="error" sx={{ mb: 2 }}>
            Não foi possível cadastrar a transação. Tente novamente.
          </Alert>
        )}

        <TransactionForm personId={personId} onSubmit={handleSubmit} onCancel={handleClose} />
      </DialogContent>
    </Dialog>
  );
}
