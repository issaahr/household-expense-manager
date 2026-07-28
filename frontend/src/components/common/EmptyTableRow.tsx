import { TableCell, TableRow } from '@mui/material';

interface EmptyTableRowProps {
  colSpan: number;
  message: string;
}

/**
 * Linha exibida quando uma tabela não possui dados,
 * evitando repetir esse markup em cada tabela do app.
 */
export function EmptyTableRow({ colSpan, message }: EmptyTableRowProps) {
  return (
    <TableRow>
      <TableCell colSpan={colSpan} align="center">
        {message}
      </TableCell>
    </TableRow>
  );
}
