/**
 * Dados enviados para criação de pessoa.
 */
export interface CreatePersonRequest {
  name: string;
  birthDate: string; // ISO date (yyyy-MM-dd)
}

/**
 * Pessoa retornada pela API.
 */
export interface PersonResponse {
  id: number;
  name: string;
  age: number;
  createdAt: string;
}
