import { render, screen, waitFor } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { describe, expect, it, vi } from 'vitest';

import { TransactionDialog } from './TransactionDialog';

describe('TransactionDialog', () => {
  it('fecha modal após criar transação com sucesso', async () => {
    const user = userEvent.setup();

    const onClose = vi.fn();

    const onSubmit = vi.fn().mockResolvedValue(undefined);

    render(<TransactionDialog open personId={10} onClose={onClose} onSubmit={onSubmit} />);

    await user.type(screen.getByLabelText('Descrição'), 'Aluguel');

    await user.type(screen.getByLabelText('Valor (R$)'), '1200');

    await user.click(
      screen.getByRole('button', {
        name: 'Cadastrar',
      }),
    );

    await waitFor(() => {
      expect(onSubmit).toHaveBeenCalled();
      expect(onClose).toHaveBeenCalled();
    });
  });

  it('mantém modal aberto e mostra erro quando criação falha', async () => {
    const user = userEvent.setup();

    const onClose = vi.fn();

    const onSubmit = vi.fn().mockRejectedValue(new Error('Erro API'));

    render(<TransactionDialog open personId={10} onClose={onClose} onSubmit={onSubmit} />);

    await user.type(screen.getByLabelText('Descrição'), 'Mercado');

    await user.type(screen.getByLabelText('Valor (R$)'), '100');

    await user.click(
      screen.getByRole('button', {
        name: 'Cadastrar',
      }),
    );

    await waitFor(() => {
      expect(screen.getByText('Não foi possível cadastrar a transação. Tente novamente.')).toBeInTheDocument();
    });

    expect(onClose).not.toHaveBeenCalled();
  });
});
