import { CssBaseline, ThemeProvider } from '@mui/material';
import { Navigate, Route, Routes } from 'react-router-dom';

import { AppLayout } from './layout/AppLayout';
import { PersonPage } from './pages/PersonPage';
import { SummaryPage } from './pages/SummaryPage';
import { theme } from './theme';

export function App() {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Routes>
        <Route element={<AppLayout />}>
          <Route path="/" element={<Navigate to="/person" />} />
          <Route path="/person" element={<PersonPage />} />
          <Route path="/summary" element={<SummaryPage />} />
        </Route>
      </Routes>
    </ThemeProvider>
  );
}
