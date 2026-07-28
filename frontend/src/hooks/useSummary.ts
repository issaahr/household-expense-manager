import { useEffect, useState } from 'react';

import { personService } from '../services/personService';
import type { PersonFinancialSummaryResponse } from '../types/summary';

/**
 * Responsável por carregar o resumo financeiro.
 */
export function useSummary() {
  const [summary, setSummary] = useState<PersonFinancialSummaryResponse | null>(null);

  const [loading, setLoading] = useState(false);

  const [error, setError] = useState(false);

  async function loadSummary() {
    setLoading(true);

    try {
      setError(false);

      const response = await personService.getFinancialSummary();

      setSummary(response);
    } catch (error) {
      if (import.meta.env.DEV) {
        console.error(error);
      }

      setError(true);
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    loadSummary();
  }, []);

  return {
    summary,
    loading,
    error,
    reload: loadSummary,
  };
}
