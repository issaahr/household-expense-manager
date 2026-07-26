import { Route, Routes } from 'react-router-dom';
import { PeoplePage } from './pages/PeoplePage';
import { SummaryPage } from './pages/SummaryPage';
import { TransactionsPage } from './pages/TransactionsPage';

export function App() {
  return (
    <Routes>
      <Route path="/people" element={<PeoplePage />} />
      <Route path="/transactions" element={<TransactionsPage />} />
      <Route path="/summary" element={<SummaryPage />} />
    </Routes>
  );
}
