import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { describe, expect, it, vi } from 'vitest';

import type { PersonResponse } from '../../types/person';
import { PersonTable } from './PersonTable';

const mockPerson: PersonResponse[] = [
  {
    id: 1,
    name: 'Maria Silva',
    age: 30,
    createdAt: '2026-01-01',
  },
  {
    id: 2,
    name: 'João Souza',
    age: 15,
    createdAt: '2026-01-01',
  },
];

describe('PersonTable', () => {
  it('renderiza estado vazio', () => {
    render(<PersonTable person={[]} onDelete={vi.fn()} onCreateTransaction={vi.fn()} />);

    expect(screen.getByText('Nenhuma pessoa cadastrada ainda.')).toBeInTheDocument();
  });

  it('lista pessoas', () => {
    render(<PersonTable person={mockPerson} onDelete={vi.fn()} onCreateTransaction={vi.fn()} />);

    expect(screen.getByText('Maria Silva')).toBeInTheDocument();

    expect(screen.getByText('João Souza')).toBeInTheDocument();
  });

  it('abre o dialog de criação de transação ao clicar em nova transação', async () => {
    const user = userEvent.setup();

    render(<PersonTable person={mockPerson} onDelete={vi.fn()} onCreateTransaction={vi.fn()} />);

    await user.click(
      screen.getAllByRole('button', {
        name: 'Nova transação',
      })[0],
    );

    expect(screen.getByRole('dialog')).toBeInTheDocument();
    expect(
      screen.getByRole('heading', {
        name: 'Nova transação',
      }),
    ).toBeInTheDocument();
  });

  it('envia uma transação vinculada à pessoa selecionada', async () => {
    const user = userEvent.setup();

    const onCreateTransaction = vi.fn().mockResolvedValue(undefined);

    render(<PersonTable person={mockPerson} onDelete={vi.fn()} onCreateTransaction={onCreateTransaction} />);

    await user.click(
      screen.getAllByRole('button', {
        name: 'Nova transação',
      })[0],
    );

    await user.type(screen.getByLabelText('Descrição'), 'Compra mercado');

    await user.type(screen.getByLabelText('Valor (R$)'), '100');

    await user.click(
      screen.getByRole('button', {
        name: 'Cadastrar',
      }),
    );

    expect(onCreateTransaction).toHaveBeenCalledWith({
      description: 'Compra mercado',
      amount: 100,
      type: 0,
      personId: 1,
    });
  });
});
