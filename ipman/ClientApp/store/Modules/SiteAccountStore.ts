import Router from '../../router';
import { SiteAccount, SiteAccountSearchCriteria } from '@entity';
import { ISiteAccountState } from '../Types';
import { storeBuilder } from './Store/Store';
import { SiteAccountService } from '../Api/Services';
import { SiteAccountType } from '../../entity/lookups/SiteAccountType';
import { PostStore } from '@store'
import { SearchSiteAccountResponse } from '@serviceModels';

const siteAccountApiService = new SiteAccountService();
const state: ISiteAccountState = 
{    
    isSearchingSiteAccountList: false,
    userSiteAccountList: [],
    searchSiteAccountList: [],
    searchCriteria: 
    {
        Keyword: '',
        OtherUserSites: false,
        CurrentUserSites: false,
        UserEmail: '',
        IncludeSiteAccountUserAccounts: false,
        ExcludedSiteAccounts: [],
        PageNumber: 0,
        PageSize: 0,
        RecordsToSkip: 0,
        SortBy: []
    },
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
        return state.userSiteAccountList;
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

    function updateUserSiteAccountList(state:ISiteAccountState, newList: SiteAccount[])
    {
        state.userSiteAccountList = newList;
    }

    function updateSearchCriteria(state: ISiteAccountState, searchCriteria: SiteAccountSearchCriteria)
    {
        state.searchCriteria = searchCriteria;
    }

    function resetSearchCriteria(state: ISiteAccountState)
    {
        mutations.updateSearchCriteria({
                Keyword: '',
                OtherUserSites: false,
                CurrentUserSites: false,
                UserEmail: '',
                IncludeSiteAccountUserAccounts: false,
                ExcludedSiteAccounts: [],
                PageNumber: 0,
                PageSize: 0,
                RecordsToSkip: 0,
                SortBy: []
            });
    }

    function updateSearchSiteAccountList(state: ISiteAccountState, newList: SiteAccount[])
    {
        state.searchSiteAccountList = newList;
    }

    export const mutations = {
        updateUserSiteAccountList: b.commit(updateUserSiteAccountList),
        updateActiveSiteAccount: b.commit(updateActiveSiteAccount),
        updateSearchCriteria: b.commit(updateSearchCriteria),
        updateSearchSiteAccountList: b.commit(updateSearchSiteAccountList),
        resetSearchCriteria: b.commit(resetSearchCriteria)
    }
}

namespace Actions {
    async function loadSiteAccount(context, site: SiteAccount)
    {
        await PostStore.actions.fetchPostList(site.ID);
        Mutations.mutations.updateActiveSiteAccount(site);
    }

    async function fetchUserSiteAccounts(context){
        Mutations.mutations.resetSearchCriteria();
        state.searchCriteria.CurrentUserSites = true;
        let apiResponse = await siteAccountApiService.Search(state.searchCriteria);

        console.log(apiResponse);
        if(!apiResponse.success)
            return; // maybe do better error handling

        let searchResponse = apiResponse.data as SearchSiteAccountResponse;
        if(searchResponse.IsError)
        {
            console.log(searchResponse.ResponseError.ToFormattedString());
            return;
        }
        Mutations.mutations.updateUserSiteAccountList(searchResponse.SiteAccounts);
    }

    async function search(context, searchCriteria: SiteAccountSearchCriteria){
        Mutations.mutations.updateSearchCriteria(searchCriteria);
        let apiResponse = await siteAccountApiService.Search(searchCriteria);
        console.log(apiResponse);
        if(!apiResponse.success)
            return; // maybe do better error handling
        
        let searchResponse = apiResponse.data as SearchSiteAccountResponse;
        if(searchResponse.IsError)
        {
            console.log(searchResponse.ResponseError.ToFormattedString());
            return;
        }

        Mutations.mutations.updateSearchSiteAccountList(searchResponse.SiteAccounts);
    }

    export const actions = {
        fetchUserSiteAccounts: b.dispatch(fetchUserSiteAccounts),
        loadSiteAccount: b.dispatch(loadSiteAccount),
        search: b.dispatch(search)
    }
}

const SiteAccountModule = {
    get state() { return stateGetter()},
    getters: Getters.getters,
    mutations: Mutations.mutations,
    actions: Actions.actions
  }
  
  
  export default SiteAccountModule;
  
  