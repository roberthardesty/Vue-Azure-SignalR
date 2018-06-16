import { Post } from "../Post";
import { Tag } from "../Tag";

export interface PostTag {
    PostID: string;
    Post: Post;
    TagID: string;
    Tag: Tag;
}