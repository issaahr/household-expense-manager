import { useState } from 'react';

import { Alert, Button, Table, TableBody, TableCell, TableHead, TableRow } from '@mui/material';

import { ConfirmDialog } from '../common/ConfirmDialog';
import { EmptyTableRow } from '../common/EmptyTableRow';
import type { PersonResponse } from '../../types/person';

interface PersonTableProps {
  person: PersonResponse[];
  onDelete: (id: number) => Promise<void>;
}

/**
 * Exibe as pessoas cadastradas e permite exclusão.
 */
export function PersonTable({ person, onDelete }: PersonTableProps) {
  const [selectedPersonId, setSelectedPersonId] = useState<number | null>(null);
  const [deleteError, setDeleteError] = useState(false);
  const [isDeleting, setIsDeleting] = useState(false);

  async function handleConfirmDelete() {
    if (selectedPersonId === null) return;

    setIsDeleting(true);
    try {
      await onDelete(selectedPersonId);
      setSelectedPersonId(null);
      setDeleteError(false);
    } catch (error) {
      if (import.meta.env.DEV) console.error('Erro ao excluir pessoa:', error);
      setDeleteError(true);
    } finally {
      setIsDeleting(false);
    }
  }

  return (
    <>
      {deleteError && (
        <Alert severity="error" onClose={() => setDeleteError(false)} sx={{ mb: 2 }}>
          Não foi possível excluir a pessoa. Tente novamente.
        </Alert>
      )}

      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Nome</TableCell>
            <TableCell>Idade</TableCell>
            <TableCell />
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
                <TableCell>
                  <Button color="error" variant="outlined" onClick={() => setSelectedPersonId(person.id)}>
                    Excluir
                  </Button>
                </TableCell>
              </TableRow>
            ))
          )}
        </TableBody>
      </Table>

      <ConfirmDialog
        open={selectedPersonId !== null}
        isConfirming={isDeleting}
        title="Excluir pessoa?"
        message="Essa ação também removerá todas as transações associadas a essa pessoa."
        onCancel={() => setSelectedPersonId(null)}
        onConfirm={handleConfirmDelete}
      />
    </>
  );
}
