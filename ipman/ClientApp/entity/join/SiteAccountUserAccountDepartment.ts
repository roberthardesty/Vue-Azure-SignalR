import SiteAccountUserAccount from "./SiteAccountUserAccount";
import Department from "../Department";

export default interface SiteAccountUserAccountDepartment {
    SiteAccountUserAccountID: string;
    SiteAccountUserAccount: SiteAccountUserAccount;
    DepartmentID: string;
    Department: Department;
}