import { PersonForm } from '../components/person/PersonForm';

import { PersonTable } from '../components/person/PersonTable';

import { usePerson } from '../hooks/usePeople';

export function PersonPage() {
  const { person, createPerson, deletePerson } = usePerson();

  return (
    <>
      <h1>Pessoas</h1>

      <PersonForm onSubmit={createPerson} />

      <PersonTable person={person} onDelete={deletePerson} />
    </>
  );
}
