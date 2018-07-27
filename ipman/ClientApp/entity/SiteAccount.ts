import { EntityBase } from "./EntityBase";
import SiteAccountUserAccount from "./join/SiteAccountUserAccount";
import Department from "./Department";
import Post from "./Post";
import Tag from "./Tag";

export default interface SiteAccount extends EntityBase
{
    SiteAccountName: string;
    SiteAccountImagePath: string; 
    SiteAccountThemeColorPrimary: string;
    SiteAccountThemeColorSecondary: string;
    SiteAccountType: any;
    IsActive: boolean;
    CreatedUTC: Date | string;
    LastUpdatedUTC: Date | string;
    SiteAccountUserAccounts: SiteAccountUserAccount[];
    Departments: Department[];
    Posts: Post[];
    Tags: Tag[];
}
