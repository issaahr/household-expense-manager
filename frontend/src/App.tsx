import { ThemeProvider, CssBaseline } from '@mui/material';
import { Route, Routes, Navigate } from 'react-router-dom';
import { theme } from './theme';
import { AppLayout } from './layout/AppLayout';
import { PersonPage } from './pages/PersonPage';
import { SummaryPage } from './pages/SummaryPage';
import { TransactionsPage } from './pages/TransactionsPage';

export function App() {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Routes>
        <Route element={<AppLayout />}>
          <Route path="/" element={<Navigate to="/person" />} />
          <Route path="/person" element={<PersonPage />} />
          <Route path="/transactions" element={<TransactionsPage />} />
          <Route path="/summary" element={<SummaryPage />} />
        </Route>
      </Routes>
    </ThemeProvider>
  );
}
