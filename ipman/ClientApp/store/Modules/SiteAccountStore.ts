import Router from '../../router';
import { SiteAccount } from '@entity';
import { ISiteAccountState } from '../Types';
import { storeBuilder } from './Store/Store';
import { SiteAccountService } from '../Api/Services';
import { SiteAccountType } from '../../entity/lookups/SiteAccountType';

const siteAccountApiService = new SiteAccountService();
const state: ISiteAccountState = 
{
    siteAccountSearchCriteria: {},
    isSearchingSiteAccountList: false,
    siteAccountList: [],
    activeSiteAccount: {
        ID: "",
        SiteAccountImagePath: "",
        SiteAccountThemeColorPrimary: "",
        SiteAccountThemeColorSecondary: "",
        SiteAccountName: "",
        IsActive: false,
        CreatedUTC: null,
        LastUpdatedUTC: null,
        SiteAccountType: SiteAccountType.Basic,
        Posts: [],
        Departments: [],
        SiteAccountUserAccounts: [],
        Tags: [],
    }
};

const b = storeBuilder.module<ISiteAccountState>("SiteAccountModule", state);
const stateGetter = b.state()

namespace Getters {
    
    const activeSiteAccount = b.read(function siteAccount(state){
        return state.activeSiteAccount;
    })

    const siteAccountList = b.read(function postList(state) {
        return state.siteAccountList;
    })
    
    export const getters = {
        get siteAccountList() {return siteAccountList()},
        get activeSiteAccount() {return activeSiteAccount()}
    } 
}

namespace Mutations {

    function updateActiveSiteAccount(state: ISiteAccountState, site: SiteAccount)
    {
        state.activeSiteAccount = site;
    }

    function updateSiteAccountList(state:ISiteAccountState, newList: SiteAccount[])
    {
        state.siteAccountList = newList;
    }

    export const mutations = {
        updateSiteAccountList: b.commit(updateSiteAccountList),
        updateActiveSiteAccount: b.commit(updateActiveSiteAccount)
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
  
  