import Post from "../Post";
import Tag from "../Tag";

export default interface PostTag {
    PostID: string;
    Post: Post;
    TagID: string;
    Tag: Tag;
}