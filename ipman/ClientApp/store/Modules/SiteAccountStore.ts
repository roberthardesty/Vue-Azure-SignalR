import Router from '../../router';
import { SiteAccount, SiteAccountSearchCriteria, SiteAccountUserAccount, Role } from '@entity';
import { ISiteAccountState } from '../Types';
import { storeBuilder } from './Store/Store';
import { SiteAccountService } from '../Api/Services';
import { SiteAccountType } from '../../entity/lookups/SiteAccountType';
import { PostStore, LoginStore } from '@store'
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

    const userSiteAccountList = b.read(function userPostList(state) {
        return state.userSiteAccountList;
    })

    const searchSiteAccountList = b.read(function searchPostList(state) {
        return state.searchSiteAccountList;
    })
    
    export const getters = {
        get userSiteAccountList() {return userSiteAccountList()},
        get activeSiteAccount() {return activeSiteAccount()},
        get searchSiteAccountList() {return searchSiteAccountList()}
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

    async function requestInvite(context, siteAccount: SiteAccount)
    {
        console.log(LoginStore.getters.user)
        let siteAccountUserAccount: SiteAccountUserAccount = {
            SiteAccountID: siteAccount.ID,
            UserAccountID: LoginStore.getters.user.ID,
            RoleID: Role.GuestRole.RoleID,
            IsActive: false,
            IsMemberOfAllDepartments: false,
            LastLoginUTC: new Date(Date.now()),
            Role: Role.GuestRole
        }

        await siteAccountApiService.Save({ 
            SiteAccount: siteAccount,
            SiteAccountUserAccounts: [siteAccountUserAccount],
            ShouldUpdateAllProps: false,
            PropsToUpdate: [] 
        });
    }

    async function updateSiteUser(context, siteUser: SiteAccountUserAccount)
    {
        let response = await siteAccountApiService.Save({
            SiteAccount: Getters.getters.activeSiteAccount,
            SiteAccountUserAccounts: [siteUser],
            ShouldUpdateAllProps: false,
            PropsToUpdate: []
        })
        console.log(response);
    }
    
    export const actions = {
        fetchUserSiteAccounts: b.dispatch(fetchUserSiteAccounts),
        loadSiteAccount: b.dispatch(loadSiteAccount),
        search: b.dispatch(search),
        requestInvite: b.dispatch(requestInvite),
        updateSiteUser: b.dispatch(updateSiteUser)
    }
}

const SiteAccountModule = {
    get state() { return stateGetter()},
    getters: Getters.getters,
    mutations: Mutations.mutations,
    actions: Actions.actions
  }
  
  
  export default SiteAccountModule;
  
  