import { PostTag } from "./join/PostTag";
import { EntityBase } from "./EntityBase";

export interface Post extends EntityBase {
    PostTitle: string;
    PostDescription: string;
    PostTag: PostTag[];
}