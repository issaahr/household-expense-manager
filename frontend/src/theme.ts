import { createTheme } from '@mui/material/styles';

/**
 * Tema centralizado do MUI. Mantém a paleta e tipografia
 * consistentes sem precisar estilizar cada componente individualmente.
 */
export const theme = createTheme({
  palette: {
    mode: 'light',
    primary: { main: '#4592c5' },
    error: { main: '#C62828' },
    background: { default: '#F5F5F5' },
  },
  shape: {
    borderRadius: 10,
  },
  typography: {
    fontFamily: '"Inter", "Roboto", sans-serif',
  },
});
