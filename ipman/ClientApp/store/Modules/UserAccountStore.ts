import Router from '../../router';
import { UserAccount } from '@entity';
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
    export const getters = {
    } 
}

namespace Mutations {
    export const mutations = {

    }
}

namespace Actions {

    async function checkUsernameAvailability(context, username: string)
    {
        console.log('in the user account store: ', username);
        let apiResponse = await userAccountService.SearchUserAccounts({Username: username, SiteAccountID: null, Email: "" });
        if(!apiResponse.success)
            return false;
        let searchResponse = apiResponse.data as SearchUserAccountsResponse;
        return !searchResponse.IsError && !searchResponse.UserAccounts.length
    }

    async function saveUserAccount(context, userAccount: UserAccount)
    {
        if(LoginStore.getters.user.ID != userAccount.ID)
            return;
        
        await userAccountService.SaveUserAccount(userAccount);
    }

    export const actions = {
        checkUsernameAvailability: b.dispatch(checkUsernameAvailability),
        saveUserAccount: b.dispatch(saveUserAccount)
    }
}

const UserAccountModule = {
    get state() { return stateGetter()},
    getters: Getters.getters,
    mutations: Mutations.mutations,
    actions: Actions.actions
  }
  
  
  export default UserAccountModule;
  
  