import {
  Button,
  CircularProgress,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from '@mui/material';

interface ConfirmDialogProps {
  open: boolean;
  title: string;
  message: string;
  isConfirming?: boolean;
  onConfirm: () => void;
  onCancel: () => void;
}

/**
 * Dialog genérico para confirmação de ações destrutivas.
 * `isConfirming` desabilita os botões e mostra um spinner enquanto
 * a ação assíncrona (ex: delete) está em andamento, evitando cliques
 * duplicados.
 */
export function ConfirmDialog({ open, title, message, isConfirming = false, onConfirm, onCancel }: ConfirmDialogProps) {
  return (
    <Dialog open={open} onClose={isConfirming ? undefined : onCancel}>
      <DialogTitle>{title}</DialogTitle>

      <DialogContent>
        <DialogContentText>{message}</DialogContentText>
      </DialogContent>

      <DialogActions>
        <Button onClick={onCancel} disabled={isConfirming}>
          Cancelar
        </Button>

        <Button
          color="error"
          variant="contained"
          onClick={onConfirm}
          disabled={isConfirming}
          startIcon={isConfirming ? <CircularProgress size={16} color="inherit" /> : undefined}
        >
          Confirmar
        </Button>
      </DialogActions>
    </Dialog>
  );
}
