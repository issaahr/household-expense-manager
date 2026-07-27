import { describe, it, expect, vi } from 'vitest';
import { render, screen, waitFor } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { PersonTable } from './PersonTable';
import type { PersonResponse } from '../../types/person';

const mockperson: PersonResponse[] = [
  { id: 1, name: 'Maria Silva', age: 30, createdAt: '2026-01-01T00:00:00Z' },
  { id: 2, name: 'João Souza', age: 15, createdAt: '2026-01-01T00:00:00Z' },
];

describe('PersonTable', () => {
  it('exibe a mensagem de estado vazio quando não há pessoas', () => {
    render(<PersonTable person={[]} onDelete={vi.fn()} />);

    expect(screen.getByText('Nenhuma pessoa cadastrada ainda.')).toBeInTheDocument();
  });

  it('lista as pessoas recebidas via props', () => {
    render(<PersonTable person={mockperson} onDelete={vi.fn()} />);

    expect(screen.getByText('Maria Silva')).toBeInTheDocument();
    expect(screen.getByText('João Souza')).toBeInTheDocument();
    expect(screen.getByText('30')).toBeInTheDocument();
    expect(screen.getByText('15')).toBeInTheDocument();
  });

  it('abre o dialog de confirmação ao clicar em excluir', async () => {
    const user = userEvent.setup();
    render(<PersonTable person={mockperson} onDelete={vi.fn()} />);

    const deleteButtons = screen.getAllByRole('button', { name: 'Excluir' });
    await user.click(deleteButtons[0]);

    expect(screen.getByText('Excluir pessoa?')).toBeInTheDocument();
    expect(
      screen.getByText(/removerá todas as transações associadas/i)
    ).toBeInTheDocument();
  });

  it('chama onDelete com o id correto ao confirmar a exclusão', async () => {
    const onDelete = vi.fn().mockResolvedValue(undefined);
    const user = userEvent.setup();

    render(<PersonTable person={mockperson} onDelete={onDelete} />);

    const deleteButtons = screen.getAllByRole('button', { name: 'Excluir' });
    await user.click(deleteButtons[0]); // pessoa id 1 (Maria Silva)

    const confirmButton = screen.getByRole('button', { name: 'Confirmar' });
    await user.click(confirmButton);

    await waitFor(() => {
      expect(onDelete).toHaveBeenCalledWith(1);
    });
  });

  it('fecha o dialog sem chamar onDelete ao cancelar', async () => {
    const onDelete = vi.fn();
    const user = userEvent.setup();

    render(<PersonTable person={mockperson} onDelete={onDelete} />);

    await user.click(screen.getAllByRole('button', { name: 'Excluir' })[0]);
    await user.click(screen.getByRole('button', { name: 'Cancelar' }));

    await waitFor(() => {
      expect(screen.queryByText('Excluir pessoa?')).not.toBeInTheDocument();
    });

    expect(onDelete).not.toHaveBeenCalled();
  });

  it('exibe mensagem de erro quando a exclusão falha', async () => {
    const onDelete = vi.fn().mockRejectedValue(new Error('Falha de rede'));
    const user = userEvent.setup();

    render(<PersonTable person={mockperson} onDelete={onDelete} />);

    await user.click(screen.getAllByRole('button', { name: 'Excluir' })[0]);
    await user.click(screen.getByRole('button', { name: 'Confirmar' }));

    await waitFor(() => {
      expect(
        screen.getByText('Não foi possível excluir a pessoa. Tente novamente.')
      ).toBeInTheDocument();
    });
  });

  it('desabilita os botões do dialog enquanto a exclusão está em andamento', async () => {
    let resolveDelete: () => void;
    const onDelete = vi.fn(
      () => new Promise<void>((resolve) => { resolveDelete = resolve; })
    );
    const user = userEvent.setup();

    render(<PersonTable person={mockperson} onDelete={onDelete} />);

    await user.click(screen.getAllByRole('button', { name: 'Excluir' })[0]);
    await user.click(screen.getByRole('button', { name: 'Confirmar' }));

    expect(screen.getByRole('button', { name: 'Cancelar' })).toBeDisabled();

    resolveDelete!();
    await waitFor(() => {
      expect(screen.queryByText('Excluir pessoa?')).not.toBeInTheDocument();
    });
  });
});
