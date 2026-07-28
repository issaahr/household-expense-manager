import { Table, TableBody, TableCell, TableHead, TableRow } from '@mui/material';

import type { TransactionResponse } from '../../types/transaction';
import { formatCurrency } from '../../utils/currency';
import { EmptyTableRow } from '../common/EmptyTableRow';

interface TransactionTableProps {
  transactions: TransactionResponse[];
}

/**
 * Exibe a lista de transações cadastradas.
 */
export function TransactionTable({ transactions }: TransactionTableProps) {
  return (
    <Table>
      <TableHead>
        <TableRow>
          <TableCell>Descrição</TableCell>

          <TableCell>Pessoa</TableCell>

          <TableCell align="right">Valor</TableCell>

          <TableCell>Tipo</TableCell>
        </TableRow>
      </TableHead>

      <TableBody>
        {transactions.length === 0 ? (
          <EmptyTableRow colSpan={4} message="Nenhuma transação cadastrada ainda." />
        ) : (
          transactions.map((transaction) => (
            <TableRow key={transaction.id}>
              <TableCell>{transaction.description}</TableCell>

              <TableCell>{transaction.personName}</TableCell>

              <TableCell align="right">{formatCurrency(transaction.amount)}</TableCell>

              <TableCell>{transaction.type === 0 ? 'Despesa' : 'Receita'}</TableCell>
            </TableRow>
          ))
        )}
      </TableBody>
    </Table>
  );
}
