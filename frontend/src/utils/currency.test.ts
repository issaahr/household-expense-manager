import { describe, expect, it } from 'vitest';

import { formatCurrency } from './currency';

function normalize(value: string) {
  return value.replace(/\u00A0/g, ' ');
}

describe('formatCurrency', () => {
  it('formata zero', () => {
    expect(normalize(formatCurrency(0))).toBe('R$ 0,00');
  });

  it('formata valores positivos', () => {
    expect(normalize(formatCurrency(1234.56))).toBe('R$ 1.234,56');
  });

  it('formata valores negativos', () => {
    expect(normalize(formatCurrency(-99.9))).toBe('-R$ 99,90');
  });
});
