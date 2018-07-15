import { UserAccount } from "@entity";

export interface SaveUserAccountRequest
{
    UserAccount: UserAccount;
    ShouldUpdateAllProps: boolean;
    PropsToUpdate: string[];
}