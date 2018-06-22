import { EntityBase } from "./EntityBase";
import UserAccount from "./UserAccount";
import PostWager from "./join/PostWager";

export default interface Wager extends EntityBase
{
    UserAccountID: string;
    UserAccount: UserAccount;
    WagerAmount: number;
    PostWagers: PostWager[];
}
