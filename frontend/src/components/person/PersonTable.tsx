import { Alert, Button, Stack, Table, TableBody, TableCell, TableHead, TableRow } from '@mui/material';
import { useState } from 'react';

import type { PersonResponse } from '../../types/person';
import type { CreateTransactionRequest } from '../../types/transaction';
import { ConfirmDialog } from '../common/ConfirmDialog';
import { EmptyTableRow } from '../common/EmptyTableRow';
import { TransactionDialog } from '../transaction/TransactionDialog';

interface PersonTableProps {
  /**
   * Pessoas cadastradas.
   */
  person: PersonResponse[];

  /**
   * Remove uma pessoa.
   */
  onDelete(id: number): Promise<void>;

  /**
   * Cria uma transação.
   */
  onCreateTransaction(request: CreateTransactionRequest): Promise<void>;
}

/**
 * Exibe a lista de pessoas cadastradas.
 *
 * Permite:
 * - excluir uma pessoa;
 * - cadastrar uma transação vinculada.
 */
export function PersonTable({ person, onDelete, onCreateTransaction }: PersonTableProps) {
  const [personToDelete, setPersonToDelete] = useState<PersonResponse | null>(null);

  const [transactionPersonId, setTransactionPersonId] = useState<number | null>(null);

  const [deleteError, setDeleteError] = useState(false);

  const [isDeleting, setIsDeleting] = useState(false);

  async function handleDelete() {
    if (!personToDelete) {
      return;
    }

    setIsDeleting(true);

    try {
      await onDelete(personToDelete.id);

      setDeleteError(false);
      setPersonToDelete(null);
    } catch (error) {
      if (import.meta.env.DEV) {
        console.error(error);
      }

      setDeleteError(true);
    } finally {
      setIsDeleting(false);
    }
  }

  return (
    <>
      {deleteError && (
        <Alert severity="error" sx={{ mb: 2 }} onClose={() => setDeleteError(false)}>
          Não foi possível excluir a pessoa.
        </Alert>
      )}

      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Nome</TableCell>

            <TableCell>Idade</TableCell>

            <TableCell align="right" width={280}>
              Ações
            </TableCell>
          </TableRow>
        </TableHead>

        <TableBody>
          {person.length === 0 ? (
            <EmptyTableRow colSpan={3} message="Nenhuma pessoa cadastrada ainda." />
          ) : (
            person.map((person) => (
              <TableRow key={person.id}>
                <TableCell>{person.name}</TableCell>

                <TableCell>{person.age}</TableCell>

                <TableCell align="right">
                  <Stack direction="row" spacing={1} sx={{ justifyContent: 'flex-end' }}>
                    <Button variant="contained" onClick={() => setTransactionPersonId(person.id)}>
                      Nova transação
                    </Button>

                    <Button color="error" variant="outlined" onClick={() => setPersonToDelete(person)}>
                      Excluir
                    </Button>
                  </Stack>
                </TableCell>
              </TableRow>
            ))
          )}
        </TableBody>
      </Table>

      <ConfirmDialog
        open={personToDelete !== null}
        isConfirming={isDeleting}
        title="Excluir pessoa?"
        message="Essa ação também removerá todas as transações associadas a essa pessoa."
        onCancel={() => setPersonToDelete(null)}
        onConfirm={handleDelete}
      />

      {transactionPersonId !== null && (
        <TransactionDialog
          open
          personId={transactionPersonId}
          onClose={() => setTransactionPersonId(null)}
          onSubmit={onCreateTransaction}
        />
      )}
    </>
  );
}
