import { EntityBase } from "./EntityBase";
import { FaceCollection } from "@entity";

export default interface Person extends EntityBase
{
    FirstName: string;
    LastName: string;
    CreatedUTC: Date | string;
    LastUpdatedUTC: Date | string;
    IsActive: boolean;
    FaceCollections: FaceCollection[];
}