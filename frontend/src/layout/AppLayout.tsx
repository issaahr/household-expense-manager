import { AppBar, Box, Container, Tab, Tabs, Toolbar, Typography } from '@mui/material';
import { Outlet, useLocation, useNavigate } from 'react-router-dom';

const ROUTES = [
  { path: '/', label: 'Pessoas' },
  { path: '/summary', label: 'Sumário Financeiro' },
];

/**
 * Layout compartilhado por todas as páginas: barra superior com
 * navegação por abas. O Outlet renderiza a página da rota ativa.
 */
export function AppLayout() {
  const navigate = useNavigate();
  const location = useLocation();

  return (
    <Box sx={{ minHeight: '100vh', bgcolor: 'background.default' }}>
      <AppBar position="static" color="primary" elevation={0}>
        <Toolbar>
          <Typography variant="h6" sx={{ flexGrow: 1, fontWeight: 600 }}>
            Household Expense Manager
          </Typography>
        </Toolbar>
        <Tabs
          value={location.pathname}
          onChange={(_, value) => navigate(value)}
          textColor="inherit"
          indicatorColor="secondary"
          sx={{ px: 2, bgcolor: 'primary.dark' }}
        >
          {ROUTES.map((route) => (
            <Tab key={route.path} value={route.path} label={route.label} />
          ))}
        </Tabs>
      </AppBar>

      <Container maxWidth="md" sx={{ py: 4 }}>
        <Outlet />
      </Container>
    </Box>
  );
}
