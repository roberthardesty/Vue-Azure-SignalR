import PostTag from "./join/PostTag";
import { EntityBase } from "./EntityBase";
import SiteAccount from "./SiteAccount";

export default interface Tag extends EntityBase {
    TagName: string;
    TagImage: string;
    SiteAccountID: string;
    SiteAccount: SiteAccount;
    IsActive: boolean;
    CreatedUTC: Date | string;
    PostTags: PostTag[];
}
