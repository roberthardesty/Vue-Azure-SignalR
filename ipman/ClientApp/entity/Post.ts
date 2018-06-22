import PostTag from "./join/PostTag";
import { EntityBase } from "./EntityBase";
import UserAccount from "./UserAccount";
import SiteAccount from "./SiteAccount";
import PostWager from "./join/PostWager";
import PostChoice from "./PostChoice";
import Vote from "./Vote";

export default interface Post extends EntityBase {
    PostTitle: string;
        PostDescription: string;
        IsActive: boolean;
        IsLocked: boolean;
        StartTimeUTC: Date | string | null;
        LockTimeUTC: Date | string | null;
        EndTimeUTC: Date | string | null;
        CreatedUTC: Date | string;
        LastUpdatedUTC: Date | string;
        SiteAccountID: string;
        SiteAccount: SiteAccount;
        UserAccountCreatorID: string;
        UserAccountCreator: UserAccount;
        Votes: Vote[];
        PostTags: PostTag[];
        PostWagers: PostWager[];
        PostChoices: PostChoice[];
}
