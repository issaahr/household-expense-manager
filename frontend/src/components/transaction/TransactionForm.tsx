import { zodResolver } from '@hookform/resolvers/zod';
import { Button, MenuItem, Stack, TextField } from '@mui/material';
import { useForm } from 'react-hook-form';

import {
  type TransactionFormInput,
  type TransactionFormValues,
  transactionSchema,
  TransactionType,
} from '../../schemas/transactionSchema';
import type { CreateTransactionRequest } from '../../types/transaction';

interface TransactionFormProps {
  /**
   * Pessoa que receberá a transação.
   */
  personId: number;

  /**
   * Executa o cadastro da transação.
   */
  onSubmit(data: CreateTransactionRequest): Promise<void>;

  /**
   * Cancela o cadastro.
   */
  onCancel(): void;
}

/**
 * Formulário responsável pelo cadastro de transações.
 */
export function TransactionForm({ personId, onSubmit, onCancel }: TransactionFormProps) {
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors, isSubmitting },
  } = useForm<TransactionFormInput, unknown, TransactionFormValues>({
    resolver: zodResolver(transactionSchema),
    defaultValues: {
      type: TransactionType.Expense,
      personId,
      amount: '',
      description: '',
    },
  });

  async function submit(data: TransactionFormValues) {
    await onSubmit(data);

    reset({
      description: '',
      amount: '',
      type: TransactionType.Expense,
      personId,
    });
  }

  return (
    <Stack component="form" spacing={2} sx={{ mt: 2 }} onSubmit={handleSubmit(submit)}>
      <TextField
        label="Descrição"
        {...register('description')}
        error={!!errors.description}
        helperText={errors.description?.message}
      />

      <TextField
        label="Valor (R$)"
        placeholder="0,00"
        inputMode="decimal"
        {...register('amount')}
        error={!!errors.amount}
        helperText={errors.amount?.message}
      />

      <TextField
        select
        label="Tipo"
        {...register('type', {
          valueAsNumber: true,
        })}
        error={!!errors.type}
        helperText={errors.type?.message}
      >
        <MenuItem value={TransactionType.Expense}>Despesa</MenuItem>

        <MenuItem value={TransactionType.Income}>Receita</MenuItem>
      </TextField>

      <Stack direction="row" spacing={2} sx={{ justifyContent: 'flex-end' }}>
        <Button type="button" variant="outlined" onClick={onCancel}>
          Cancelar
        </Button>

        <Button type="submit" variant="contained" disabled={isSubmitting}>
          Cadastrar
        </Button>
      </Stack>
    </Stack>
  );
}
