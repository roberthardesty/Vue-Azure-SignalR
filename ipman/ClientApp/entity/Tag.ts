import { PostTag } from "./join/PostTag";
import { EntityBase } from "./EntityBase";

export interface Tag extends EntityBase {
    TagName: string;
    PostTags: PostTag[];
}