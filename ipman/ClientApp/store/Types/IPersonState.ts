import { Person, PersonSearchCriteria } from "@entity";

export interface IPersonState
{
    currentPerson: Person;
    personList: Person[];
    searchCriteria: PersonSearchCriteria;
    isSearching: boolean;
}