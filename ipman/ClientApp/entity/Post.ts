import PostTag from "./join/PostTag";
import { EntityBase } from "./EntityBase";

export default interface Post extends EntityBase {
    PostTitle: string;
    PostDescription: string;
    PostTag: PostTag[];
}