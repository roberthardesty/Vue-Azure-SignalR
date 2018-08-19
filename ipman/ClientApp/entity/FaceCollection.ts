import { EntityBase } from "./EntityBase";
import { FaceImage, Person } from "@entity";

export default interface FaceCollection extends EntityBase{
    PersonID: string;
    Person: Person;
    FaceImages: FaceImage[];
    IsActive: boolean;
    CreatedUTC: Date | string;
    LastUpdatedUTC: Date | string;
}