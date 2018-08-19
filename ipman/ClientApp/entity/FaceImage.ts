import { EntityBase } from "./EntityBase";
import { FaceCollection } from "@entity";

export default interface FaceImage extends EntityBase{
    FaceCollectionID: string;
    FaceCollection: FaceCollection;
    BlobPath: string;
    CreatedUTC: Date | string;
    IsActive: boolean;
}