import { UserAccount } from "@entity";

export interface ILoginState
{
    isLoadingUserContext: boolean,
    userContext: {
        isLoggedIn: boolean,
        user: UserAccount
    }
}