import { render, screen } from '@testing-library/react';
import { describe, expect, it } from 'vitest';

import { SummaryTable } from './SummaryTable';

describe('SummaryTable', () => {
  it('renderiza estado vazio quando summary é nulo', () => {
    render(<SummaryTable summary={null} />);

    expect(screen.getByText('Nenhuma pessoa cadastrada ainda.')).toBeInTheDocument();
  });

  it('renderiza estado vazio quando não existem pessoas', () => {
    render(
      <SummaryTable
        summary={{
          people: [],
          totalIncome: 0,
          totalExpense: 0,
          balance: 0,
        }}
      />,
    );

    expect(screen.getByText('Nenhuma pessoa cadastrada ainda.')).toBeInTheDocument();
  });

  it('renderiza pessoas e resumo financeiro', () => {
    render(
      <SummaryTable
        summary={{
          people: [
            {
              personId: 1,
              name: 'Maria',
              totalIncome: 500,
              totalExpense: 100,
              balance: 400,
            },
            {
              personId: 2,
              name: 'João',
              totalIncome: 100,
              totalExpense: 250,
              balance: -150,
            },
          ],
          totalIncome: 600,
          totalExpense: 350,
          balance: 250,
        }}
      />,
    );

    expect(screen.getByText('Maria')).toBeInTheDocument();
    expect(screen.getByText('João')).toBeInTheDocument();

    expect(screen.getByText('Total Geral')).toBeInTheDocument();

    expect(screen.getByText('-R$ 150,00')).toBeInTheDocument();
    expect(screen.getByText('R$ 400,00')).toBeInTheDocument();
  });
});
