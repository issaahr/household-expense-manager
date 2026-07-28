import { describe, expect, it } from 'vitest';

import { personSchema } from './personSchema';

describe('personSchema', () => {
  it('aceita uma pessoa com nome e data de nascimento válidos', () => {
    const result = personSchema.safeParse({
      name: 'Maria Silva',
      birthDate: '1990-05-10',
    });

    expect(result.success).toBe(true);
  });

  it('rejeita nome com menos de 2 caracteres', () => {
    const result = personSchema.safeParse({
      name: 'A',
      birthDate: '1990-05-10',
    });

    expect(result.success).toBe(false);
  });

  it('rejeita nome vazio', () => {
    const result = personSchema.safeParse({
      name: '',
      birthDate: '1990-05-10',
    });

    expect(result.success).toBe(false);
  });

  it('rejeita nome contendo apenas espaços', () => {
    const result = personSchema.safeParse({
      name: '   ',
      birthDate: '1990-05-10',
    });

    expect(result.success).toBe(false);
  });

  it('aceita nome com espaços externos removendo o excesso', () => {
    const result = personSchema.safeParse({
      name: '  Maria Silva  ',
      birthDate: '1990-05-10',
    });

    expect(result.success).toBe(true);

    if (result.success) {
      expect(result.data.name).toBe('Maria Silva');
    }
  });

  it('rejeita nome com mais de 150 caracteres', () => {
    const result = personSchema.safeParse({
      name: 'A'.repeat(151),
      birthDate: '1990-05-10',
    });

    expect(result.success).toBe(false);
  });

  it('rejeita data de nascimento no futuro', () => {
    const futureDate = new Date();

    futureDate.setFullYear(futureDate.getFullYear() + 1);

    const result = personSchema.safeParse({
      name: 'Maria Silva',
      birthDate: futureDate.toISOString().split('T')[0],
    });

    expect(result.success).toBe(false);
  });

  it('rejeita data de nascimento que implique idade superior a 150 anos', () => {
    const tooOldDate = new Date();

    tooOldDate.setFullYear(tooOldDate.getFullYear() - 151);

    const result = personSchema.safeParse({
      name: 'Maria Silva',
      birthDate: tooOldDate.toISOString().split('T')[0],
    });

    expect(result.success).toBe(false);
  });
});
