import { SiteAccountUserAccount } from "./SiteAccountUserAccount";
import { Department } from "../Department";

export interface SiteAccountUserAccountDepartment {
    SiteAccountUserAccountID: string;
    SiteAccountUserAccount: SiteAccountUserAccount;
    DepartmentID: string;
    Department: Department;
}