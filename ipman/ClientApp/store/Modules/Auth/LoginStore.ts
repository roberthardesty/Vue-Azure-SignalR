import Router from '../../../router';
import { storeBuilder } from '../Store/Store';
import { ILoginState } from '../../Types';
import { UserAccount, AuthenticationProvider } from '@entity';
import { JWT } from './TokenStore';
import { AuthService, UserAccountService } from '../../Api/Services';
import { ValidateUserContextRequest, ValidateUserContextResponse } from '@serviceModels';
import { EventBus } from '@store';

const state: ILoginState = 
{
    isLoadingUserContext: false,

    userContext: {
        isLoggedIn: false,
        user: {
            ID: "",
            EmailAddress: "",
            Username: "",
            FirstName: "",
            LastName: "",
            GitHubID: "",
            GoogleID: "",
            AvatarLink: "",
            IsActive: false,
            LastLoginProvider: null,
            LastLoginUTC: "",
            CreatedUTC: "",
            LastUpdatedUTC: "",
            Votes: [],
            Wagers: [],
            CreatedPosts: [],
            SiteAccountUserAccounts: [],
            SiteAccounts: [],
        }
    }
};

const b = storeBuilder.module<ILoginState>("LoginModule", state);
const stateGetter = b.state()

const authService = new AuthService();
const userService = new UserAccountService();

namespace Getters {
    const isLoading = b.read(function isLoading(state: ILoginState){
        return state.isLoadingUserContext;
    });

    const isLoggedin = b.read(function isLoggedin(state: ILoginState){
        return state.userContext.isLoggedIn;
    })

    const user = b.read(function user(state: ILoginState){
        return state.userContext.user;
    })

    export const getters = {
        get user() { return user()},
        get isLoading() {return isLoading()},
        get isLoggedIn() { return isLoggedin()}
    } 
}

namespace Mutations {

    function updateUser(state:ILoginState, user: UserAccount)
    {
        state.userContext.user = user;
    }

    function updateLoadingState(state: ILoginState, isLoading: boolean)
    {
        state.isLoadingUserContext = isLoading;
    }

    export const mutations = {
        updateUser: b.commit(updateUser),
        updateLoadingState: b.commit(updateLoadingState)
    }
}

namespace Actions {

    async function initializeUserContext(context, data: {email: string, refresh_token: string})
    {
        Mutations.mutations.updateLoadingState(true);
        await buildAndSendValidateionRequest({refresh_token: data.refresh_token, email_token: data.email}, async (response) =>
        {
            validationSuccess(response);
            if(!Getters.getters.user.Username)
            {
                state.userContext.user.Username = await requestUsername();
                console.log(state.userContext.user)
                userService.SaveUserAccount({ UserAccount: state.userContext.user, PropsToUpdate: ["Username"], ShouldUpdateAllProps: false } );
            }
        });
        Mutations.mutations.updateLoadingState(false);
    }

    async function validateUserContext(context) 
    {
        Mutations.mutations.updateLoadingState(true);
        let tokens = JWT.fetch();
        await buildAndSendValidateionRequest(tokens, validationSuccess);
        Mutations.mutations.updateLoadingState(false);
    }

    async function buildAndSendValidateionRequest(tokens: {refresh_token: string, email_token: string},
                                                  callback: (response: ValidateUserContextResponse) => void )
    {
        if(!!tokens.refresh_token)
        {
            let validateUserContextRequest: ValidateUserContextRequest = 
            { 
                Token : tokens.refresh_token,
                Email: tokens.email_token
            };
            let apiReponse = await authService.ValidateUserContext(validateUserContextRequest);
            if(apiReponse.success)
            {
                let validationResponse = apiReponse.data as ValidateUserContextResponse;
                if(validationResponse.IsError)
                {
                    console.log(validationResponse.ResponseError.ToFormattedString());
                }
                else
                    callback(validationResponse)
            }
        }
    }

    function validationSuccess(response: ValidateUserContextResponse)
    {
        JWT.set({email_token: response.UserAcount.EmailAddress, refresh_token: response.NewToken})
        Mutations.mutations.updateUser(response.UserAcount)
        state.userContext.isLoggedIn = true;
    }

    function requestUsername(): Promise<string>
    {
        return new Promise((resolve,reject) => 
        {
            EventBus.$once("username_popup_close", (username: string) =>
            {
                resolve(username);
            });
            EventBus.$emit("username_popup_open");
        })
    }

    export const actions = {
        validateUserContext: b.dispatch(validateUserContext),
        initializeUserContext: b.dispatch(initializeUserContext)
    }
}

const LoginModule = {
    get state() { return stateGetter()},
    getters: Getters.getters,
    mutations: Mutations.mutations,
    actions: Actions.actions
  }
  
  
  export default LoginModule;
  
  