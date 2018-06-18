import PostTag from "./join/PostTag";
import { EntityBase } from "./EntityBase";

export default interface Tag extends EntityBase {
    TagName: string;
    PostTags: PostTag[];
}