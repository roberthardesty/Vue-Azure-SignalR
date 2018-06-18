import { EntityBase } from "./EntityBase";
import SiteAccountUserAccount from "./join/SiteAccountUserAccount";
import SiteAccountUserAccountDepartment from "./join/SiteAccountUserAccountDepartment";

export default interface SiteAccount extends EntityBase
{
    SiteAccountName: string;
    IsActive: boolean;
    LastUpdatedUTC: Date | string;
    SiteAccountUserAccount: SiteAccountUserAccount[];
    SiteAccountUserAccountDepartment: SiteAccountUserAccountDepartment[];
}