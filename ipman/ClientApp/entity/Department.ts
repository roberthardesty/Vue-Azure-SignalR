import { EntityBase } from "./EntityBase";
import { SiteAccount } from "./SiteAccount";
import { SiteAccountUserAccountDepartment } from "./join/SiteAccountUserAccountDepartment";

export interface Department extends EntityBase {
    DepartmentName: string;
    CreatedUTC: Date | string;
    LastUpdatedUTC: Date | string;
    IsActive: boolean;
    SiteAccount: SiteAccount;
    SiteAccountUserAccountDepartment: SiteAccountUserAccountDepartment[];
}