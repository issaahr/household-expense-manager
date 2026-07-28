import { Button, TextField, Stack } from '@mui/material';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { personSchema, type PersonFormValues } from '../../schemas/personSchema';
import type { CreatePersonRequest } from '../../types/person';

interface PersonFormProps {
  onSubmit(data: CreatePersonRequest): Promise<void>;
}

/**
 * Formulário responsável pelo cadastro de pessoas.
 */
export function PersonForm({ onSubmit }: PersonFormProps) {
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<PersonFormValues>({
    resolver: zodResolver(personSchema),
  });

  async function submit(data: PersonFormValues) {
    await onSubmit(data);
    reset();
  }

  return (
    <Stack component="form" spacing={2} onSubmit={handleSubmit(submit)}>
      <TextField label="Nome" {...register('name')} error={!!errors.name} helperText={errors.name?.message} />

      <TextField
        label="Data de nascimento"
        type="date"
        slotProps={{ inputLabel: { shrink: true } }}
        {...register('birthDate')}
        error={!!errors.birthDate}
        helperText={errors.birthDate?.message}
      />

      <Button type="submit" variant="contained">
        Cadastrar
      </Button>
    </Stack>
  );
}
