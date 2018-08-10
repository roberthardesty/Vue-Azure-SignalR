import Router from '../../router';
import { UserAccount, Role } from '@entity';
import { IUserAccountState } from '../Types';
import { UserAccountService } from '../Api/Services';
import { LoginStore } from '@store';
import { storeBuilder } from './Store/Store'
import { SearchUserAccountsResponse } from '../Api/ServiceModels/SearchUserAccountsResponse';


const state: IUserAccountState = 
{
    userAccountSearchCriteria: {},
    isSearchingUserAccountList: false,
    userAccountList: []
};

const b = storeBuilder.module<IUserAccountState>("UserAccountModule", state);
const stateGetter = b.state()
const userAccountService = new UserAccountService();


namespace Getters {
    const userAccountList = b.read(function userAccountList(state){
        return state.userAccountList;
    })
    export const getters = {
        get userAccountList(){ return userAccountList() }
    } 
}

namespace Mutations {
    function updateUserAccountList(state: IUserAccountState, users: UserAccount[])
    {
        state.userAccountList = users;
    }
    export const mutations = {
        updateUserAccountList: b.commit(updateUserAccountList)
    }
}

namespace Actions {

    async function checkUsernameAvailability(context, username: string)
    {
        console.log('in the user account store: ', username);
        let apiResponse = await userAccountService.SearchUserAccounts({
            Username: username,
            SiteAccountID: null,
            UserEmail: "",
            IncludeSiteAccountUserAccounts: false,
            PageNumber: 0,
            PageSize: 0,
            RecordsToSkip: 0,
            SortBy: [],
        });
        if(!apiResponse.success)
            return false;
        let searchResponse = apiResponse.data as SearchUserAccountsResponse;
        return !searchResponse.IsError && !searchResponse.UserAccounts.length
    }

    async function loadPendingUsersForSite(context, siteID: string)
    {
        let apiResponse = await userAccountService.SearchUserAccounts({
            Username: "",
            SiteAccountID: siteID,
            Roles: [ Role.GuestRole ],
            UserEmail: "",
            IncludeSiteAccountUserAccounts: true,
            PageNumber: 0,
            PageSize: 0,
            RecordsToSkip: 0,
            SortBy: [],
           });
    
        if(!apiResponse.success)
           return false;
       
        let searchResponse = apiResponse.data as SearchUserAccountsResponse;
       
        if(searchResponse.IsError)
           return false;
        Mutations.mutations.updateUserAccountList(searchResponse.UserAccounts);
    }

    async function saveUserAccount(context, userAccount: UserAccount)
    {
        if(LoginStore.getters.user.ID != userAccount.ID)
            return;
        
        await userAccountService.SaveUserAccount({ UserAccount: userAccount, ShouldUpdateAllProps: true, PropsToUpdate: [] });
    }

    export const actions = {
        checkUsernameAvailability: b.dispatch(checkUsernameAvailability),
        saveUserAccount: b.dispatch(saveUserAccount),
        loadPendingUsersForSite: b.dispatch(loadPendingUsersForSite)
    }
}

const UserAccountModule = {
    get state() { return stateGetter()},
    getters: Getters.getters,
    mutations: Mutations.mutations,
    actions: Actions.actions
  }
  
  
  export default UserAccountModule;
  
  