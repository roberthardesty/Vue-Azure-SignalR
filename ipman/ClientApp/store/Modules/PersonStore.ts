import PersonService from "../Api/Services/PersonService";
import { IPersonState } from "../Types";
import { FaceCollection, PersonSearchCriteria, Person } from "@entity";
import { storeBuilder } from "./Store/Store";

const personService = new PersonService();
const personState: IPersonState = 
{
    currentPerson: {
        ID: "",
        FirstName: "",
        LastName: "",
        CreatedUTC: "",
        LastUpdatedUTC: "",
        IsActive: false,
        FaceCollections: [],
    },
    searchCriteria: {
        IncludedPersonIDs: [],
        ExcludedPersonIDs: [],
        Keyword: "",
        CommunityID: "",
        PageNumber: 0,
        PageSize: 0,
        RecordsToSkip: 0,
        SortBy: []
    },
    isSearching: false,
    personList: [],
}

const b = storeBuilder.module<IPersonState>("PersonModule", personState);
const stateGetter = b.state()


namespace Getters {
    
    const currentPerson = b.read(function currentPerson(state){
        return state.currentPerson;
    })

    const personList = b.read(function personList(state) {
        return state.personList;
    })

    export const getters = {
        get personList() {return personList()},
        get currentPerson() {return currentPerson()}
    } 
}

namespace Mutations {

    function updateCurrentPerson(state: IPersonState, person: Person)
    {
        state.currentPerson = person;
    }

    function updatePersonList(state:IPersonState, newList: Person[])
    {
        state.personList = newList;
    }

    function updateSearchCriteria(state: IPersonState, searchCriteria: PersonSearchCriteria)
    {
        state.searchCriteria = searchCriteria;
    }

    function resetSearchCriteria(state: IPersonState)
    {
        mutations.updateSearchCriteria({
                IncludedPersonIDs: [],
                ExcludedPersonIDs: [],
                Keyword: "",
                CommunityID: "",
                PageNumber: 0,
                PageSize: 0,
                RecordsToSkip: 0,
                SortBy: []
            });
    }

    export const mutations = {
        updatePersonList: b.commit(updatePersonList),
        updateCurrentPerson: b.commit(updateCurrentPerson),
        updateSearchCriteria: b.commit(updateSearchCriteria),
        resetSearchCriteria: b.commit(resetSearchCriteria)
    }
}

namespace Actions {
    
   async function savePerson(context, person: Person)
   {
       if(!person.ID.length) person.ID = "00000000-0000-0000-0000-000000000000"
       if(!person.CreatedUTC.toString().length) person.CreatedUTC = new Date(Date.now());
       person.LastUpdatedUTC = new Date(Date.now());
       console.log(person);
       await personService.Save({ Person: person });
   }
    
    export const actions = {
        savePerson: b.dispatch(savePerson),
        // loadSiteAccount: b.dispatch(loadSiteAccount),
        // search: b.dispatch(search),
        // requestInvite: b.dispatch(requestInvite),
        // updateSiteUser: b.dispatch(updateSiteUser)
    }
}

const PersonModule = {
    get state() { return stateGetter()},
    getters: Getters.getters,
    mutations: Mutations.mutations,
    actions: Actions.actions
  }
  
  
  export default PersonModule;
  