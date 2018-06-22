import { EntityBase } from "./EntityBase";
import SiteAccount from "./SiteAccount";
import { AuthenticationProvider } from "./lookups/AuthenticationProvider";
import SiteAccountUserAccount from "./join/SiteAccountUserAccount";
import Post from "./Post";
import Vote from "./Vote";
import Wager from "./Wager";

export default interface UserAccount extends EntityBase {
    EmailAddress: string;
    Username: string;
    FirstName: string;
    LastName: string;
    GitHubID: string;
    GoogleID: string;
    AvatarLink: string;
    IsActive: boolean;
    LastLoginProvider: AuthenticationProvider;
    LastLoginUTC: Date | string;
    CreatedUTC: Date | string;
    LastUpdatedUTC: Date | string;
    Votes: Vote[];
    Wagers: Wager[];
    CreatedPosts: Post[];
    SiteAccountUserAccounts: SiteAccountUserAccount[];
    SiteAccounts: SiteAccount[];
}
