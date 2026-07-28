import { z } from 'zod';

const MAX_AGE_YEARS = 150;

/**
 * Validação do formulário de pessoa, espelhando as regras aplicadas
 * pelo CreatePersonRequestValidator no backend (FluentValidation):
 * - Nome: 2 a 150 caracteres.
 * - Data de nascimento: deve ser anterior a hoje e não pode implicar
 *   idade superior a 150 anos.
 */
export const personSchema = z.object({
  name: z
    .string()
    .trim()
    .min(2, 'Nome deve possuir pelo menos 2 caracteres')
    .max(150, 'Nome deve possuir no máximo 150 caracteres'),
  birthDate: z
    .string()
    .min(1, 'Data de nascimento é obrigatória')
    .refine((date) => new Date(date) < new Date(), {
      message: 'Data de nascimento deve ser anterior à data atual',
    })
    .refine(
      (date) => {
        const minDate = new Date();

        minDate.setFullYear(minDate.getFullYear() - MAX_AGE_YEARS);

        return new Date(date) > minDate;
      },
      {
        message: `A idade não pode ser superior a ${MAX_AGE_YEARS} anos`,
      },
    ),
});

export type PersonFormValues = z.infer<typeof personSchema>;
