import { describe, it, expect, vi } from 'vitest';
import { render, screen, waitFor } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { PersonForm } from './PersonForm';

/**
 * Testes para o componente PersonForm.
 */
describe('PersonForm', () => {
  it('chama onSubmit com os dados preenchidos', async () => {
    const onSubmit = vi.fn().mockResolvedValue(undefined);
    const user = userEvent.setup();

    render(<PersonForm onSubmit={onSubmit} />);

    await user.type(screen.getByLabelText('Nome'), 'João Souza');
    await user.type(screen.getByLabelText('Data de nascimento'), '1995-03-20');
    await user.click(screen.getByRole('button', { name: 'Cadastrar' }));

    await waitFor(() => {
      expect(onSubmit).toHaveBeenCalledWith({
        name: 'João Souza',
        birthDate: '1995-03-20',
      });
    });
  });

  it('não chama onSubmit quando o nome está vazio', async () => {
    const onSubmit = vi.fn();
    const user = userEvent.setup();

    render(<PersonForm onSubmit={onSubmit} />);

    await user.click(screen.getByRole('button', { name: 'Cadastrar' }));

    await waitFor(() => {
      expect(screen.getByText('Nome deve possuir pelo menos 2 caracteres')).toBeInTheDocument();
    });

    expect(onSubmit).not.toHaveBeenCalled();
  });
});
