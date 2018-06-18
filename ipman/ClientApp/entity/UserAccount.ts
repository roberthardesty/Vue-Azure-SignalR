import { EntityBase } from "./EntityBase";
import SiteAccount from "./SiteAccount";
import Department from "./Department";
import { AuthenticationProvider } from "./lookups/AuthenticationProvider";
import SiteAccountUserAccount from "./join/SiteAccountUserAccount";
import SiteAccountUserAccountDepartment from "./join/SiteAccountUserAccountDepartment";

export default interface UserAccount extends EntityBase
{
     EmailAddress: string;
     FirstName: string;
     LastName: string;
     GitHubID: string;
     GoogleID: string;
     AvatarLink: string;
     LastLoginProvider: AuthenticationProvider;
     LastLoginUTC: Date | string;
     CreatedUTC: Date | string;
     LastUpdatedUTC: Date | string;
     SiteAccountUserAccounts: SiteAccountUserAccount[];
     SiteAccountUserAccountDepartments: SiteAccountUserAccountDepartment[];
     SiteAccounts: SiteAccount[];
     Departmets: Department[];
 }
