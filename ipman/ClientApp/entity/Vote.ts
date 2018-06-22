import { EntityBase } from "./EntityBase";
import Post from "./Post";
import UserAccount from "./UserAccount";

export default interface Vote extends EntityBase
{
    PostID: string;
    Post: Post;
    UserAccountID: string;
    UserAccount: UserAccount;
    CreatedUTC: Date | string;
    IsUpTally: boolean;
    IsComment: boolean;
}
