import { render, screen } from '@testing-library/react';
import { describe, expect, it } from 'vitest';

import { TransactionTable } from './TransactionTable';

describe('TransactionTable', () => {
  it('renderiza estado vazio quando não existem transações', () => {
    render(<TransactionTable transactions={[]} />);

    expect(screen.getByText('Nenhuma transação cadastrada ainda.')).toBeInTheDocument();
  });

  it('renderiza as transações cadastradas', () => {
    render(
      <TransactionTable
        transactions={[
          {
            id: 1,
            description: 'Conta de luz',
            amount: 150,
            type: 0,
            personId: 1,
            personName: 'Maria',
          },
          {
            id: 2,
            description: 'Salário',
            amount: 3000,
            type: 1,
            personId: 1,
            personName: 'Maria',
          },
        ]}
      />,
    );

    expect(screen.getByText('Conta de luz')).toBeInTheDocument();
    expect(screen.getByText('Salário')).toBeInTheDocument();

    expect(screen.getAllByText('Maria')).toHaveLength(2);

    expect(screen.getByText('R$ 150,00')).toBeInTheDocument();
    expect(screen.getByText('R$ 3.000,00')).toBeInTheDocument();

    expect(screen.getByText('Despesa')).toBeInTheDocument();
    expect(screen.getByText('Receita')).toBeInTheDocument();
  });
});
