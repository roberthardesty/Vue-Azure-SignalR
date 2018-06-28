import Router from '../../router';
import { SiteAccount } from '@entity';
import { ISiteAccountState } from '../Types';
import { storeBuilder } from './Store/Store';
import { SiteAccountService } from '../Api/Services';

const siteAccountApiService = new SiteAccountService();
const state: ISiteAccountState = 
{
    siteAccountSearchCriteria: {},
    isSearchingSiteAccountList: false,
    siteAccountList: []
};

const b = storeBuilder.module<ISiteAccountState>("SiteAccountModule", state);
const stateGetter = b.state()

namespace Getters {
    const siteAccountList = b.read(function postList(state) {
        return state.siteAccountList;
    })
    
    export const getters = {
        get siteAccountList() {return siteAccountList()}
    } 
}

namespace Mutations {
    function updateSiteAccountList(state:ISiteAccountState, newList: SiteAccount[])
    {
        state.siteAccountList = newList;
    }

    export const mutations = {
        updateSiteAccountList: b.commit(updateSiteAccountList)
    }
}

namespace Actions {
    async function fetchUserSiteAccounts(context){
        let response = await siteAccountApiService.GetUserSiteAccounts();
        console.log(response);
        if(!response.success)
            return; // maybe do better error handling
        Mutations.mutations.updateSiteAccountList(response.data as SiteAccount[])
    }
    export const actions = {
        fetchUserSiteAccounts: b.dispatch(fetchUserSiteAccounts)
    }
}

const SiteAccountModule = {
    get state() { return stateGetter()},
    getters: Getters.getters,
    mutations: Mutations.mutations,
    actions: Actions.actions
  }
  
  
  export default SiteAccountModule;
  
  