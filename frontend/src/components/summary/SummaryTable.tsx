import { Table, TableBody, TableCell, TableHead, TableRow } from '@mui/material';

import type { PersonFinancialSummaryResponse } from '../../types/summary';
import { formatCurrency } from '../../utils/currency';
import { EmptyTableRow } from '../common/EmptyTableRow';

interface SummaryTableProps {
  summary: PersonFinancialSummaryResponse | null;
}

/**
 * Exibe o resumo financeiro
 * de todas as pessoas cadastradas.
 */
export function SummaryTable({ summary }: SummaryTableProps) {
  return (
    <Table>
      <TableHead>
        <TableRow>
          <TableCell>Pessoa</TableCell>

          <TableCell align="right">Receitas</TableCell>

          <TableCell align="right">Despesas</TableCell>

          <TableCell align="right">Saldo</TableCell>
        </TableRow>
      </TableHead>

      <TableBody>
        {!summary || summary.people.length === 0 ? (
          <EmptyTableRow colSpan={4} message="Nenhuma pessoa cadastrada ainda." />
        ) : (
          <>
            {summary.people.map((person) => (
              <TableRow key={person.personId}>
                <TableCell>{person.name}</TableCell>

                <TableCell align="right">{formatCurrency(person.totalIncome)}</TableCell>

                <TableCell align="right">{formatCurrency(person.totalExpense)}</TableCell>

                <TableCell align="right">{formatCurrency(person.balance)}</TableCell>
              </TableRow>
            ))}

            <TableRow>
              <TableCell>
                <strong>Total Geral</strong>
              </TableCell>

              <TableCell align="right">
                <strong>{formatCurrency(summary.totalIncome)}</strong>
              </TableCell>

              <TableCell align="right">
                <strong>{formatCurrency(summary.totalExpense)}</strong>
              </TableCell>

              <TableCell align="right">
                <strong>{formatCurrency(summary.balance)}</strong>
              </TableCell>
            </TableRow>
          </>
        )}
      </TableBody>
    </Table>
  );
}
