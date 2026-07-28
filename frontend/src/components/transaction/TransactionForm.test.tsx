import { render, screen, waitFor } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { describe, expect, it, vi } from 'vitest';

import { TransactionType } from '../../schemas/transactionSchema';
import { TransactionForm } from './TransactionForm';

describe('TransactionForm', () => {
  it('envia uma transação válida convertendo o valor para número', async () => {
    const user = userEvent.setup();

    const onSubmit = vi.fn().mockResolvedValue(undefined);

    render(<TransactionForm personId={1} onSubmit={onSubmit} onCancel={vi.fn()} />);

    await user.type(screen.getByLabelText('Descrição'), 'Conta de energia');

    await user.type(screen.getByLabelText('Valor (R$)'), '150,50');

    await user.click(
      screen.getByRole('button', {
        name: 'Cadastrar',
      }),
    );

    await waitFor(() => {
      expect(onSubmit).toHaveBeenCalledWith({
        description: 'Conta de energia',
        amount: 150.5,
        type: TransactionType.Expense,
        personId: 1,
      });
    });
  });

  it('exibe erro quando valor é inválido', async () => {
    const user = userEvent.setup();

    const onSubmit = vi.fn();

    render(<TransactionForm personId={1} onSubmit={onSubmit} onCancel={vi.fn()} />);

    await user.type(screen.getByLabelText('Descrição'), 'Teste');

    await user.type(screen.getByLabelText('Valor (R$)'), '0');

    await user.click(
      screen.getByRole('button', {
        name: 'Cadastrar',
      }),
    );

    await waitFor(() => {
      expect(screen.getByText('O valor da transação deve ser positivo')).toBeInTheDocument();
    });

    expect(onSubmit).not.toHaveBeenCalled();
  });

  it('chama cancelamento', async () => {
    const user = userEvent.setup();

    const onCancel = vi.fn();

    render(<TransactionForm personId={1} onSubmit={vi.fn()} onCancel={onCancel} />);

    await user.click(
      screen.getByRole('button', {
        name: 'Cancelar',
      }),
    );

    expect(onCancel).toHaveBeenCalled();
  });
});
