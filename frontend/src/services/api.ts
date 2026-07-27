import axios from 'axios';

/**
 * Instância única do axios, com a base URL vindo de variável de ambiente.
 * Evita repetição de URL base em cada chamada e centraliza configuração futura.
 */
export const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});
