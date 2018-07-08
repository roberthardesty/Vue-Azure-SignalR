import Router from '../../router';
import { UserAccount } from '@entity';
import { IUserAccountState } from '../Types';
import { UserAccountService } from '../Api/Services';
import { storeBuilder } from './Store/Store';

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
        let apiResponse = await userAccountService.SearchUserAccounts({username: username});
        console.log(apiResponse);
    }

    export const actions = {
        checkUsernameAvailability: b.dispatch(checkUsernameAvailability)
    }
}

const UserAccountModule = {
    get state() { return stateGetter()},
    getters: Getters.getters,
    mutations: Mutations.mutations,
    actions: Actions.actions
  }
  
  
  export default UserAccountModule;
  
  